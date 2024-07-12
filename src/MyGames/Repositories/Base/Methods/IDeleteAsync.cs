using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyGames.Repositories.Base.Methods
{
    public interface IDeleteAsync<TEntity>
    {
        public Task DeleteAsync(TEntity entity);
    }
}