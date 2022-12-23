using LibraryApp.Data.Dtos;
using LibraryApp.Data.Entities;

namespace LibraryApp.Data.Services;

public interface ICartService
{
    Task<List<CartItemDto>> GetCartItemsAsync(int userId);
    Task AddItemToCartAsync(int bookId, int userId);
    Task<OrderDto?> CreateOrderAsync(int userId);
    Task<IssueOfBook?> RequestFreeBookAsync(int bookId, int userId);
    Task<IssueOfBook?> ReturnFreeBookAsync(int bookId, int userId);
}