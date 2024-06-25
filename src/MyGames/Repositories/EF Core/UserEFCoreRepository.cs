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
    public class UserEFCoreRepository : IUserRepository
    {
        private readonly MyGamesDbContext dbContext;
        public UserEFCoreRepository(MyGamesDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task CreateAsync(User user)
        {
            await dbContext.Users.AddAsync(user);
            await dbContext.SaveChangesAsync();
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            return await dbContext.Users.FirstOrDefaultAsync(user => user.Id == id);
        }

        public async Task<User?> Login(User userToLog)
        {
            return await dbContext.Users.FirstOrDefaultAsync(user => user.Login == userToLog.Login && user.Password == userToLog.Password);
        }
    }
}