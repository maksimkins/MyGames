using Microsoft.EntityFrameworkCore;
using MyGames.Models;

public class MyGamesDbContext : DbContext {
    public DbSet<User> Users { get; set; }
    public DbSet<Game> Games { get; set; }

    public MyGamesDbContext()
    {
    //    this.Database.EnsureCreated();
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

        optionsBuilder.UseSqlServer(connectionString: @"Server=localhost;Database=MyGames;Trusted_Connection=True;TrustServerCertificate=True");//User Id=admin;Password=admin;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Game>()
        .HasIndex(g => g.Name)
        .IsUnique();

        base.OnModelCreating(modelBuilder);
    }
}