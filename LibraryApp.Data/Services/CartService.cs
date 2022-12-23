using AutoMapper;
using LibraryApp.Data.Dtos;
using LibraryApp.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibraryApp.Data.Services;

public class CartService : ICartService
{
    private readonly DatabaseContext _context;
    private readonly IMapper _mapper;

    public CartService(DatabaseContext context, IMapper mapper)
    {
        _mapper = mapper;
        _context = context;
    }

    public async Task<List<CartItemDto>> GetCartItemsAsync(int userId)
    {
        var cart = await _context.Cart.FirstOrDefaultAsync(x => x.OrderDate == null && x.UserId == userId);
        if(cart is null)
        {
            await CreateCartAsync(userId);
            cart = _context.Cart.First(x => x.OrderDate == null && x.UserId == userId);
        }
        var cartItems = _context.CartItem.Where(x => x.CartId == cart.Id).ToList();
        var list = new List<CartItemDto>();

        foreach(var item in cartItems)
        {
            var book = _context.Book.Find(item.BookId);
            list.Add(new CartItemDto
            {
                Title = book.Title,
                Author = book.Author,
                Price = book.Price,
                Count = item.Count
            });
        }
        return list;
    }

    private async Task CreateCartAsync(int userId)
    {
        var item = await _context.Cart.FirstOrDefaultAsync(x => x.OrderDate == null && x.UserId == userId);
        if (item is null)
        {
            var cart = new Cart()
            {
                UserId = userId,
                OrderDate = null,
                Total = null
            };
            await _context.Cart.AddAsync(cart);
            await _context.SaveChangesAsync();
        }
    }

    public async Task AddItemToCartAsync(int bookId, int userId)
    {
        await CreateCartAsync(userId);
        var cartId = _context.Cart.First(x => x.OrderDate == null && x.UserId == userId).Id;
        var item = await _context.CartItem.FirstOrDefaultAsync(x => x.CartId == cartId && x.BookId == bookId);
        var book = await _context.Book.FindAsync(bookId);

        if(book is null || book.Price == 0)
        {
            return;
        }

        if (item is null)
        {
            item = new CartItem()
            {
                BookId = bookId,
                CartId = cartId,
                Count = 1
            };
            await _context.CartItem.AddAsync(item);
        }
        else
        {
            item.Count += 1;
            _context.Update(item);
        }

        await _context.SaveChangesAsync();
    }

    public async Task<OrderDto?> CreateOrderAsync(int userId)
    {
        var cart = _context.Cart.FirstOrDefault(x => x.OrderDate == null && x.UserId == userId);
        if(cart is null)
            return null;

        var cartItems = _context.CartItem.Where(x => x.CartId == cart.Id).ToList();
        var paidItems = new List<CartItem>();
        double total = 0;

        foreach (var item in cartItems)
        {
            var book = await _context.Book.FindAsync(item.BookId);
            if(book.Price > 0)
            {
                total += book.Price * item.Count;
                paidItems.Add(item);
            }
        }

        cart.Total = (int)total;
        cart.OrderDate = DateTime.Now;

        var order = new OrderDto();
        _mapper.Map(cart, order);
        order.CartItems = paidItems;

        _context.Cart.Update(cart);
        await _context.SaveChangesAsync();

        return order;
    }

    public async Task<IssueOfBook?> RequestFreeBookAsync(int bookId, int userId)
    {
        if(_context.IssueOfBook.Where(x => x.UserId == userId && x.ReturnDate == null).Count() >= 2)
        {
            return null;
        }

        var newRequest = new IssueOfBook();
        newRequest.FromDate = DateTime.Now;
        newRequest.TillDate = DateTime.Now.AddDays(30);
        newRequest.UserId = userId;
        newRequest.BookId = bookId;

        await _context.IssueOfBook.AddAsync(newRequest);
        await _context.SaveChangesAsync();
        return newRequest;
    }

    public async Task<IssueOfBook?> ReturnFreeBookAsync(int bookId, int userId)
    {
        var requestOrder = await _context.IssueOfBook
            .FirstOrDefaultAsync(x => x.UserId == userId && x.BookId == bookId && x.ReturnDate == null);

        if(requestOrder is not null)
        {
            requestOrder.ReturnDate = DateTime.Now;
            _context.IssueOfBook.Update(requestOrder);
            await _context.SaveChangesAsync();
        }

        return requestOrder;
    }
}
