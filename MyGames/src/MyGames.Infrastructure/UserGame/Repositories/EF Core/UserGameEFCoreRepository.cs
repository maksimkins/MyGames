#pragma warning disable CS1998

using Microsoft.EntityFrameworkCore;

namespace MyGames.Infrastructure.UserGame.Repositories.Ef_Core;

using MyGames.Core.UserGame.Repositories.Base;
using MyGames.Infrastructure.Data.DbContext;
using MyGames.Core.UserGame.Models;
using MyGames.Core.Game.Models;

public class UserGameEFCoreRepository : IUserGameRepository
{
    private readonly MyGamesDbContext dbContext;
    public UserGameEFCoreRepository(MyGamesDbContext dbContext)
    {
        this.dbContext = dbContext;
    }
    public async Task CreateAsync(UserGame userGame)
    {
        await dbContext.UserGames.AddAsync(userGame);
        await dbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<Game?>> GetAllUsersGames(int userId)
    {
        return dbContext.UserGames.Where(ug => ug.UserId == userId).Include(ug => ug.Game).Select(ug => ug.Game);
    }

    public async Task<bool> HasUserGame(int gameId, int userId)
    {
        return dbContext.UserGames.Any(ug => ug.UserId == userId && ug.GameId == gameId);
    }
}
