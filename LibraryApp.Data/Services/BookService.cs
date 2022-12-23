
using AutoMapper;
using LibraryApp.Data.Dtos;
using LibraryApp.Data.Entities;
using Microsoft.AspNetCore.Http;

namespace LibraryApp.Data.Services
{
    public class BookService : IBookService
    {
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;

        public static string WebRootPath = Path.Combine("wwwroot", "BookImages");

        public BookService(DatabaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<Book> GetPaidBooks()
        {
            return _context.Book.Where(x => x.Price > 0).ToList();
        }

        public List<Book> GetFreeBooks()
        {
            return _context.Book.Where(x => x.Price == 0).ToList();
        }

        public async Task<Book?> GetBookAsync(int id)
        {
            var book = await _context.Book.FindAsync(id);
            if(book is null)
            {
                return null;
            }
            return book;
        }

        public async Task<Book> CreateBookAsync(BookDto dto)
        {
            var fileName = await CreateFileAsync(dto.Image);
            Book book = new();
            _mapper.Map(dto, book);
            book.Image = fileName;
            await _context.Book.AddAsync(book);
            await _context.SaveChangesAsync();

            return book;
        }

        public async Task<Book> UpdateBookAsync(int id, BookDto dto)
        {
            var item = await _context.Book.FindAsync(id);
            if(item is not null)
            {
                var file = new FileInfo(Path.Combine(WebRootPath, item.Image));
                file.Delete();

                var fileName = await CreateFileAsync(dto.Image);
                _mapper.Map(dto, item);
                item.Image = fileName;

                _context.Book.Update(item);
                await _context.SaveChangesAsync();
            }
            return item;
        }

        public async Task DeleteBookAsync(int id)
        {
            var book = _context.Book.Find(id);
            if (book is not null)
            {
                _context.Remove(book);
                await _context.SaveChangesAsync();
            }
        }

        private async Task<string> CreateFileAsync(IFormFile image)
        {
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(image.FileName)}";
            var path = Path.Combine(WebRootPath, fileName);
            using (var stream = new FileStream(path, FileMode.Create))
            {
                await image.CopyToAsync(stream);
            }
            return fileName;
        }
    }
}
