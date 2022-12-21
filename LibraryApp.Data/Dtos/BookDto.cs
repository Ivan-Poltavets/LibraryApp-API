using Microsoft.AspNetCore.Http;

namespace LibraryApp.Data.Dtos;

public class BookDto
{
    public string Title { get; set; }
    public string Description { get; set; }
    public string Genre { get; set; }
    public string? Author { get; set; }
    public int Count { get; set; }
    public int AvailableCount { get; set; }
    public IFormFile Image { get; set; }
    public double Price { get; set; }
}
