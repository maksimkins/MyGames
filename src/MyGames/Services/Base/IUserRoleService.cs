using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyGames.Models;

namespace MyGames.Services.Base
{
    public interface IUserRoleService
    {
        public Task ChangeAsync(int id, Role role);
        public Task CreateAsync(UserRole userRole);
        public Task DeleteAsync(UserRole userRole);
        public Task<IEnumerable<Role?>> GetAllRolesByUserId(int userId);
    }
}