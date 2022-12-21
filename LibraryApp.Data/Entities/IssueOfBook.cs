namespace LibraryApp.Data.Entities;

public class IssueOfBook
{
    public int Id { get; set; }
    public DateOnly FromDate { get; set; }
    public DateOnly TillDate { get; set; }
    public DateOnly ReturnDate { get; set; }
    public int BookId { get; set; }
    public int UserId { get; set; }
}
