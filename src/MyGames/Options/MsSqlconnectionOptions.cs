using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyGames.Options.Base;

namespace MyGames.Options
{
    public class MsSqlconnectionOptions : IConnectionStringOption
    {
        public string? ConnectionString
        { 
            get {
                string trusted_Connection = Trusted_Connection is null ? "" : $"Trusted_Connection={Trusted_Connection};";
                string trustServerCertificate = TrustServerCertificate is null ? "" : $"TrustServerCertificate={TrustServerCertificate};";

                return $"Server={Server};Database={Database};{trusted_Connection}{trustServerCertificate}";
            } 

        }

        public string? Server { get; set; }
        public string? Database { get; set; }
        public bool? Trusted_Connection { get; set; }
        public bool? TrustServerCertificate { get; set; }
    }
}