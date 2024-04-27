using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyGames.Repositories.Base.Methods
{
    public interface IDelete<TEntity>
    {
        public void Delete(TEntity entity);
    }
}