using LibraryApp.Data.Dtos;
using LibraryApp.Data.Entities;

namespace LibraryApp.Data.Services
{
    public interface IBookService
    {
        void CreateBookAsync(BookDto dto);
        void DeleteBookAsync(int id);
        List<Book> GetFreeBooks();
        List<Book> GetPaidBooks();
        Task<Book> GetBookAsync(int id);
    }
}