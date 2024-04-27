using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyGames.Repositories.Base.Methods
{
    public interface IChangeAsync<TEntity>
    {
        public Task ChangeAsync(int id, TEntity entity);
    }
}