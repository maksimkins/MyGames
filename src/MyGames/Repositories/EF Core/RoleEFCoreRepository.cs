using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyGames.Models;
using MyGames.Repositories.Base;
using MyGames.Repositories.EF_Core.DbContext;

namespace MyGames.Repositories.EF_Core
{
    public class RoleEFCoreRepository : IRoleRepository
    {
        private readonly MyGamesDbContext dbContext;
        public RoleEFCoreRepository(MyGamesDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Role?> GetByNameAsync(string name)
        {
            return await dbContext.Roles.Where(r => r.Name == name).FirstOrDefaultAsync();
        }
    }
}