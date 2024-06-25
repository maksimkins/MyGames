using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace MyGames.Repositories.EF_Core.DbContext;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MyGames.Models;
using MyGames.Options;
using MyGames.Options.Base;

public class MyGamesDbContext : DbContext
{
    private readonly IConnectionStringOption connectionStringOptions;
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Game> Games { get; set; }
    public DbSet<Log> Logs { get; set; }
    public DbSet<User> Users { get; set; }

    public MyGamesDbContext(IOptionsSnapshot<MsSqlconnectionOptions> option)
    {
        this.connectionStringOptions = option.Value;
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

        optionsBuilder.UseSqlServer(connectionString: connectionStringOptions.ConnectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Game>()
        .HasKey(g => g.Id);
        modelBuilder.Entity<Game>()
        .Property(g => g.Name)
        .IsRequired();
        modelBuilder.Entity<Game>()
        .Property(g => g.Price)
        .IsRequired();
        modelBuilder.Entity<Game>()
        .Property(g => g.Rate)
        .HasDefaultValue(0)
        .IsRequired();

        modelBuilder.Entity<Comment>()
        .HasKey(c => c.Id);
        modelBuilder.Entity<Comment>()
        .Property(c => c.Text)
        .IsRequired();
        modelBuilder.Entity<Comment>()
        .Property(c => c.Title)
        .IsRequired();
        modelBuilder.Entity<Comment>()
        .Property(c => c.Rate)
        .IsRequired();
        modelBuilder.Entity<Comment>()
        .Property(c => c.GameId)
        .IsRequired();

        modelBuilder.Entity<User>()
        .HasKey(c => c.Id);
        modelBuilder.Entity<User>()
        .Property(c => c.Login)
        .IsRequired();
        modelBuilder.Entity<User>()
        .HasIndex(c => c.Login)
        .IsUnique();
        modelBuilder.Entity<User>()
        .Property(c => c.Password)
        .IsRequired();
        modelBuilder.Entity<User>()
        .Property(c => c.Email)
        .IsRequired();
        modelBuilder.Entity<User>()
        .HasIndex(c => c.Email)
        .IsUnique();
        modelBuilder.Entity<User>()
        .Property(c => c.Username)
        .IsRequired();
        modelBuilder.Entity<User>()
        .Property(c => c.Birthdate)
        .IsRequired();

        base.OnModelCreating(modelBuilder);
    }
}