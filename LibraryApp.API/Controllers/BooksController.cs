﻿using Microsoft.AspNetCore.Mvc;
using LibraryApp.Data.Services;
using Microsoft.AspNetCore.Authorization;
using LibraryApp.Data.Dtos;

namespace LibraryApp.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet("freebooks/")]
        public IActionResult GetFreeBooks()
        {
            var content = _bookService.GetFreeBooks();
            return Ok(content);
        }

        [HttpGet("paidbooks/")]
        public IActionResult GetPaidBooks()
        {
            var content = _bookService.GetPaidBooks();
            return Ok(content);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookAsync(int id)
        {
            var book = await _bookService.GetBookAsync(id);
            if (book is not null)
                return Ok(book);

            return NotFound(id);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBookAsync([FromForm] BookDto dto)
        {
            var book = await _bookService.CreateBookAsync(dto);
            return Ok(book);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBookAsync(int id){
            await _bookService.DeleteBookAsync(id);
            return NoContent();
        }
    }
}
