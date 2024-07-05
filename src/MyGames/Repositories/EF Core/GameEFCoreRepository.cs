using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyGames.Models;
using MyGames.Repositories.Base;
using MyGames.Repositories.EF_Core.DbContext;

namespace MyGames.Repositories.EF_Core;

#pragma warning disable CS1998

public class GameEFCoreRepository : IGameRepository
{
    private readonly MyGamesDbContext dbContext;
    public GameEFCoreRepository(MyGamesDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task DeleteAsync(Game? game)
    {
        dbContext.Games.Remove(game!);
        await dbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<Game>> GetAllAsync()
    {
        return dbContext.Games;
    }

    public async Task<IEnumerable<Game?>> GetAllFromUserLibraryAsync(User user)
    {
        return dbContext.UserGames.Where(ug => ug.UserId == user.Id).Include(ug => ug.Game).Select(ug => ug.Game);
    }

    public async Task<Game?> GetByIdAsync(int id)
    {
        return await dbContext.Games.FirstOrDefaultAsync(game => game.Id == id);
    }

    public async Task<IEnumerable<Game?>> GetGamesPagination(int page = 1, int pageSize = 10)
    {
        return dbContext.Games.Skip((page - 1) * pageSize).Take(pageSize);
    }

    public async Task<IEnumerable<Game?>> GetTopTenMostHighRated()
    {
        return dbContext.Games.OrderBy(g => g.Rate).Take(10);
    }

    public async Task<IEnumerable<Game?>> GetTopTenNewest()
    {
        return dbContext.Games.OrderBy(g => g.CreationDate).Take(10);
    }
}
