using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using MyGames.Models;
using MyGames.Repositories.Base;

namespace MyGames.Repositories.Dapper
{
    public class LogDapperRepository : ILogRepository
    {
        private const string connectionString = @"Server=localhost;Database=MyGames;Trusted_Connection=True;TrustServerCertificate=True";
        public async Task CreateAsync(Log log)
        {
            var connection = new SqlConnection(connectionString);
            await connection.ExecuteAsync(@"insert into Logs
                                        (Url, RequestBody, ResponsetBody, CreationDate, EndDate, StatusCode, HttpMethod)
                                        values (@Url, @RequestBody, @ResponsetBody, @CreationDate, @EndDate, @StatusCode, @HttpMethod)", log);
        }
    }
}