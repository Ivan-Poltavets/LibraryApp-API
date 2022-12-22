namespace LibraryApp.Data.Entities;

public class User
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Surname { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public int Bonuses { get; set; }
    public int Antibonuses { get; set; }
}
