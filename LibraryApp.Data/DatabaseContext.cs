using LibraryApp.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibraryApp.Data;

public class DatabaseContext : DbContext
{
	public DbSet<User> User { get; set; }
    public DbSet<Book> Book { get; set; }
    public DbSet<CartItem> CartItem { get; set; }
    public DbSet<Cart> Cart { get; set; }
    public DbSet<Feedback> Feedback { get; set; }
    public DbSet<IssueOfBook> IssueOfBook { get; set; }

    public DatabaseContext(DbContextOptions<DatabaseContext> options)
		: base(options)
	{

	}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CartItem>(entity =>
        {
            entity.HasKey(x => x.CartId);
            entity.HasKey(x => x.BookId);
        });
        base.OnModelCreating(modelBuilder);
    }
}
