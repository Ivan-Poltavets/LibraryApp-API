
using LibraryApp.Data.Entities;

namespace LibraryApp.Data.Dtos;

public class OrderDto
{
    public List<CartItem> CartItems { get; set; }
    public DateTime OrderDate { get; set; }
    public int Total { get; set; }
}
