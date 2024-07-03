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
    public class UserRoleEFCoreRepository : IUserRoleRepository
    {
        private readonly MyGamesDbContext dbContext;
        public UserRoleEFCoreRepository(MyGamesDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task ChangeAsync(int id, Role role)
        {
            var userRole = await dbContext.UserRoles.FirstOrDefaultAsync(ur => ur.Id == id);
            
            if(userRole is null)
                return;

            userRole.RoleId = role.Id;
            dbContext.UserRoles.Update(userRole);

            await dbContext.SaveChangesAsync();
        }

        public async Task CreateAsync(UserRole userRole)
        {
            await dbContext.UserRoles.AddAsync(userRole!);
            await dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(UserRole userRole)
        {
            dbContext.Remove(userRole);
            await dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Role?>> GetAllRolesByUserId(int userId)
        {
            var userroles = dbContext.UserRoles.Where(c => c.UserId == userId).Include(c => c.Role).Select(ur => ur.Role);
            return userroles;
        }
    }
}