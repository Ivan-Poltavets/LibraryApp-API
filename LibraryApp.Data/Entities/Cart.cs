namespace LibraryApp.Data.Entities;

public class Cart
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public DateTime? OrderDate { get; set; }
    public int? Total { get; set; }

}
