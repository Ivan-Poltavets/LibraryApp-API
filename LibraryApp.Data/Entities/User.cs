namespace LibraryApp.Data.Entities;

public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    public int Bonuses { get; set; }
    public int Antibonuses { get; set; }
}
