using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyGames.Models;
using MyGames.Repositories.Base;
using MyGames.Repositories.EF_Core.DbContext;

#pragma warning disable CS1998

namespace MyGames.Repositories.EF_Core
{
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
}