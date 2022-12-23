namespace LibraryApp.Data.Entities;

public class IssueOfBook
{
    public int Id { get; set; }
    public DateTime FromDate { get; set; }
    public DateTime TillDate { get; set; }
    public DateTime? ReturnDate { get; set; }
    public int BookId { get; set; }
    public int UserId { get; set; }
}
