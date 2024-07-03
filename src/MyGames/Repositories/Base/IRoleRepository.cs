using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyGames.Models;
using MyGames.Repositories.Base.Methods;

namespace MyGames.Repositories.Base
{
    public interface IRoleRepository 
    {
        public Task<Role?> GetByNameAsync(string name);
    }
}