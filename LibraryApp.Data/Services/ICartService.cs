namespace LibraryApp.Data.Services
{
    public interface ICartService
    {
        Task AddItemToCartAsync(int bookId, int userId);
    }
}