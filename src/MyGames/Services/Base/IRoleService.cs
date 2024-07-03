using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyGames.Models;

namespace MyGames.Services.Base
{
    public interface IRoleService
    {
        public Task<Role?> GetByNameAsync(string name);
    }
}