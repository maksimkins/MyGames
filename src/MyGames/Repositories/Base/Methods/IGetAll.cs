using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyGames.Repositories.Base
{
    public interface IGetAll<TEntity>
    {
        public IEnumerable<TEntity?> GetAll();
    }
}