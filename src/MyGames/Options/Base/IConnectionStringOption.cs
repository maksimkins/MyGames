using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyGames.Options.Base
{
    public interface IConnectionStringOption
    {
        public string? ConnectionString { get; }
    }
}