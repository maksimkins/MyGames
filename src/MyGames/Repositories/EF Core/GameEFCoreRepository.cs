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
    public async Task<IEnumerable<Game>> GetAllAsync()
    {
        System.Console.WriteLine(dbContext.Games);
        return dbContext.Games;
    }

    public async Task<Game?> GetByIdAsync(int id)
    {
        return await dbContext.Games.FirstOrDefaultAsync(game => game.Id == id);
    }
}
