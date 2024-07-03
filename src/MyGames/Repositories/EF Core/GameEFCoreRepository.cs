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

    // public async Task AddToLibrary(int userId, int gameId)
    // {
    //     dbContext.US
    //     await dbContext.Games.Where(g => g.Id == gameId).ForEachAsync(g => g.Users.Add(userId));
    // }

    public async Task<IEnumerable<Game>> GetAllFromUserLibraryAsync(User user)
    {
        throw new Exception();
    }

    public async Task<Game?> GetByIdAsync(int id)
    {
        return await dbContext.Games.FirstOrDefaultAsync(game => game.Id == id);
    }
}
