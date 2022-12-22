
namespace LibraryApp.Data.Entities;

public class Book
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string? Author { get; set; }
    public string Genre { get; set; } = string.Empty;
    public int Count { get; set; }
    public int AvailableCount { get; set; }
    public string Image { get; set; } = string.Empty;
    public double Price { get; set; }
}
