namespace MyGames.Core.Options.MsSqlConnection;

using MyGames.Core.Options.Base;

public class MsSqlConnectionOptions : IConnectionStringOption
{
    public string? ConnectionString
    { 
        get {
            string trusted_Connection = Trusted_Connection is null ? "" : $"Trusted_Connection={Trusted_Connection};";
            string trustServerCertificate = TrustServerCertificate is null ? "" : $"TrustServerCertificate={TrustServerCertificate};";
            string user_id = User_Id is null ? "" : $"User Id={User_Id};";
            string password = Password is null ? "" : $"Password={Password};";

            string connectionString = $"Server={Server};Database={Database};{trusted_Connection}{trustServerCertificate}{user_id}{password}";

            return connectionString;
        } 

    }

    public string? Server { get; set; }
    public string? Database { get; set; }
    public string? User_Id { get; set; }
    public string? Password { get; set; }
    public bool? Trusted_Connection { get; set; }
    public bool? TrustServerCertificate { get; set; }
}
