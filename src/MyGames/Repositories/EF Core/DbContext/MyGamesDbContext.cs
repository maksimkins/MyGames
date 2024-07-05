using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace MyGames.Repositories.EF_Core.DbContext;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MyGames.Models;
using MyGames.Options;
using MyGames.Options.Base;
using MyGames.Repositories.EF_Core.Configurations;


public class MyGamesDbContext : IdentityDbContext<User, Role, int>
{
    private readonly IConnectionStringOption connectionStringOptions;
    public DbSet<Comment> Comments { get; set; }
    public DbSet<UserGame> UserGames { get; set; }
    public DbSet<Game> Games { get; set; }
    public DbSet<Log> Logs { get; set; }
    public MyGamesDbContext(IOptionsSnapshot<MsSqlconnectionOptions> option, DbContextOptions options) : base(options)
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
        modelBuilder.ApplyConfiguration(new GameConfiguration());
        modelBuilder.ApplyConfiguration(new CommentConfiguration());
        modelBuilder.ApplyConfiguration(new UserGameConfiguration());
        modelBuilder.ApplyConfiguration(new UserConfiguration());

        foreach (var property in modelBuilder.Model.GetEntityTypes()
                .SelectMany(t => t.GetProperties())
                .Where(p => p.ClrType == typeof(decimal) || p.ClrType == typeof(decimal?)))
            {        
                property.SetColumnType("decimal(18,2)");  
            }
        
        base.OnModelCreating(modelBuilder);
    }
}