using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace MyGames.Infrastructure.Data.DbContext;

using MyGames.Core.Game.Models;
using MyGames.Core.Log.Models;
using MyGames.Core.UserGame.Models;
using MyGames.Core.Role.Models;
using MyGames.Core.Comment.Models;
using MyGames.Core.User.Models;

using MyGames.Core.Options.Base;
using MyGames.Core.Options.MsSqlConnection;

using MyGames.Core.User.Data.Configurations;
using MyGames.Core.Comment.Data.Configurations;
using MyGames.Core.Game.Data.Configurations;
using MyGames.Core.UserGame.Data.Configurations;

public class MyGamesDbContext : IdentityDbContext<User, Role, int>
{
    private readonly IConnectionStringOption connectionStringOptions;
    public DbSet<Comment> Comments { get; set; }
    public DbSet<UserGame> UserGames { get; set; }
    public DbSet<Game> Games { get; set; }
    public DbSet<Log> Logs { get; set; }
    public MyGamesDbContext(IOptionsSnapshot<MsSqlConnectionOptions> option, DbContextOptions options) : base(options)
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