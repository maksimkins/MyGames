using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyGames.Models;
using MyGames.Repositories.Base;
using MyGames.Services.Base;

namespace MyGames.Services
{
    public class UserRoleService : IUserRoleService
    {
        private readonly IUserRoleRepository repository;
        public UserRoleService(IUserRoleRepository repository) {
            this.repository = repository;
        }
        public async Task ChangeAsync(int id, Role role)
        {
            if(role is null || id <= 0)
            {
                throw new ArgumentException("role or id are wrong");
            }

            await repository.ChangeAsync(id, role);
        }

        public async Task CreateAsync(UserRole userRole)
        {
            if(userRole is null)
            {
                throw new ArgumentNullException("userRole is null");
            }

            await repository.CreateAsync(userRole);
        }

        public async Task DeleteAsync(UserRole userRole)
        {
            if(userRole is null)
            {
                throw new ArgumentNullException("userRole is null");
            }

            await repository.DeleteAsync(userRole);
        }

        public async Task<IEnumerable<Role?>> GetAllRolesByUserId(int userId)
        {
            if(userId <= 0)
            {
                throw new ArgumentNullException("userId in wrong");
            }

            return await repository.GetAllRolesByUserId(userId);
        }
    }
}