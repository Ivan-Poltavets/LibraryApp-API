using LibraryApp.Data;
using LibraryApp.Data.Entities;
using LibraryApp.Data.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly DatabaseContext _context;
        public BooksController(DatabaseContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetBooks()
        {
            var content = _context.Book.ToList();
            return Ok(content);
        }

        [HttpGet("{id}")]
        public IActionResult GetBook(int id)
        {
            return Ok();
        }

        [HttpPost]
        public IActionResult CreateBook([FromForm]BookDto dto)
        {
            var book = new Book();
            book.Author = dto.Author;
            book.Title = dto.Title;
            book.Price = dto.Price;
            book.Count = dto.Count;
            book.AvailableCount = dto.AvailableCount;
            book.Description = dto.Description;
            using(var memoryStream = new MemoryStream())
            {
                dto.Image.CopyTo(memoryStream);
                book.Image = memoryStream.ToArray();
            }
            _context.Book.Add(book);
            _context.SaveChanges();
            return CreatedAtAction(nameof(CreateBook), book);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            var book = _context.Book.Find(id);
            _context.Remove(book);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
