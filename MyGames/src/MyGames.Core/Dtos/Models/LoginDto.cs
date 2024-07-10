namespace MyGames.Core.Dtos.Models;

public class LoginDto
{
    public string? Username{set; get;}
    public string? Password{set; get;}
    public string? ReturnUrl { get; set; }
}
