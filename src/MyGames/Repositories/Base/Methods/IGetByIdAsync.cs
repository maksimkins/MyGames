using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyGames.Repositories.Base.Methods
{
    public interface IGetByIdAsync<TEntity>
    {
        public Task<TEntity> GetByIdAsync(int id);
    }
}