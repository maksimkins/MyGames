using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyGames.Models;
using MyGames.Repositories.Base;
using MyGames.Services.Base;

namespace MyGames.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository repository;
        public RoleService(IRoleRepository repository) {
            this.repository = repository;
        }
        public async Task<Role?> GetByNameAsync(string name)
        {
            if(string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("name cannot be empty");
            }

            return await repository.GetByNameAsync(name);
        }
    }
}