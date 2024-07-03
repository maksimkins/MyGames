using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyGames.Models;
using MyGames.Repositories.Base.Methods;

namespace MyGames.Repositories.Base
{
    public interface IUserRoleRepository : ICreateAsync<UserRole>, IDeleteAsync<UserRole>
    {
        public Task<IEnumerable<Role?>> GetAllRolesByUserId(int userId);
        public Task ChangeAsync(int id, Role role);
    }
}