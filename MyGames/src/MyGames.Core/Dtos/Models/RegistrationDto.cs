namespace MyGames.Core.Dtos.Models;

public class RegistrationDto
{
    public string? Username {set; get;}
    public string? Email{set; get;}
    public string? Password{set; get;}
    public DateTime? Birthdate {set; get;}
    public string? ReturnUrl { get; set; }
}
