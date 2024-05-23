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
    public DbSet<Comment> Users { get; set; }
    public DbSet<Game> Games { get; set; }

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
        // modelBuilder.Entity<Game>()
        // .HasIndex(g => g.Name)
        // .IsUnique();

        base.OnModelCreating(modelBuilder);
    }
}
