using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyGames.Core.Base.Methods
{
    public interface IGetAllAsync<TEntity>
    {
        public Task<IEnumerable<TEntity>> GetAllAsync();
    }
}