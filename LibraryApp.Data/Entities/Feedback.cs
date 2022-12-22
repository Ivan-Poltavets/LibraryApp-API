namespace LibraryApp.Data.Entities;

public class Feedback
{
    public int Id { get; set; }
    public string Text { get; set; } = string.Empty;
    public int Ranked { get; set; }
    public int BookId { get; set; }
    public int UserId { get; set; }
}
