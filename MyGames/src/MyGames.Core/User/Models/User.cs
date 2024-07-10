using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace MyGames.Core.User.Models;

using MyGames.Core.UserGame.Models;

public class User : IdentityUser<int>
{
    [Column(TypeName = "decimal(18,2)")]
    public decimal? Balance{set; get;}
    [Required]
    public DateTime? Birthdate {set; get;}
    public string? AvatarUrl {set; get;}
    public IEnumerable<UserGame>? Games {set; get;}
}
