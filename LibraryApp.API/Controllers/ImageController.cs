using LibraryApp.Data;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApp.API.Controllers;

[ApiController]
public class ImageController : ControllerBase
{
    private readonly DatabaseContext _context;
    private readonly IWebHostEnvironment _environment;

    public ImageController(DatabaseContext context, IWebHostEnvironment environment)
    {
        _context = context;
        _environment = environment;
    }

    [HttpGet]
    [Route("api/books/{id:int}/image")]
    public async Task<IActionResult> GetImageAsync(int id)
    {
        var book = await _context.Book.FindAsync(id);

        if(book is not null)
        {
            string path = Path.Combine(_environment.WebRootPath, "BookImages", book.Image);
            string mimetype = GetImageMimeTypeFromFileExtension(Path.GetExtension(book.Image));

            var buffer = System.IO.File.ReadAllBytes(path);
            
            return File(buffer, mimetype);
        }

        return NotFound();
    }

    private string GetImageMimeTypeFromFileExtension(string extension)
    {
        string mimetype = extension switch
        {
            ".png" => "image/png",
            ".gif" => "image/gif",
            ".jpg" or ".jpeg" => "image/jpeg",
            ".bmp" => "image/bmp",
            ".tiff" => "image/tiff",
            ".wmf" => "image/wmf",
            ".jp2" => "image/jp2",
            ".svg" => "image/svg+xml",
            _ => "application/octet-stream",
        };
        return mimetype;
    }
}
