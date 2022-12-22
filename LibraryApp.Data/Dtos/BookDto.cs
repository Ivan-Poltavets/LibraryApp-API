using Microsoft.AspNetCore.Http;

namespace LibraryApp.Data.Dtos;

public class BookDto
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Genre { get; set; } = string.Empty;
    public string? Author { get; set; }
    public int Count { get; set; }
    public int AvailableCount { get; set; }
    public IFormFile? Image { get; set; }
    public double Price { get; set; }
}
