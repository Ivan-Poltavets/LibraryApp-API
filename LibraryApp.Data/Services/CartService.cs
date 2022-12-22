using LibraryApp.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibraryApp.Data.Services;

public class CartService : ICartService
{
    private readonly DatabaseContext _context;

    public CartService(DatabaseContext context)
    {
        _context = context;
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
}
