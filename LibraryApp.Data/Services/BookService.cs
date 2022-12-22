
using AutoMapper;
using LibraryApp.Data.Dtos;
using LibraryApp.Data.Entities;

namespace LibraryApp.Data.Services
{
    public class BookService : IBookService
    {
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;

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

        public async Task<Book> GetBookAsync(int id)
        {
            return await _context.Book.FindAsync(id);
        }

        public async Task<Book> CreateBookAsync(BookDto dto)
        {
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(dto.Image.FileName)}";
            var path = Path.Combine("wwwroot", "BookImages", fileName);
            using (var stream = new FileStream(path, FileMode.Create))
            {
                await dto.Image.CopyToAsync(stream);
            }
            Book book = new();
            book = _mapper.Map(dto, book);
            book.Image = fileName;
            await _context.Book.AddAsync(book);
            await _context.SaveChangesAsync();

            return book;
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
    }
}
