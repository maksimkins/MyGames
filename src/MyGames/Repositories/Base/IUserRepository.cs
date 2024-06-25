using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyGames.Models;
using MyGames.Repositories.Base.Methods;

namespace MyGames.Repositories.Base
{
    public interface IUserRepository : ICreateAsync<User>, IGetByIdAsync<User?>
    {
        public Task<User?> Login(User userToLog);
    }
}