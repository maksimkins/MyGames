using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyGames.Core.UserGame.Models;

using MyGames.Core.User.Models;
using MyGames.Core.Game.Models;

public class UserGame
{
    [Key]
    public int Id { get; set; }
    [ForeignKey("UserId"), Required]
    public int? UserId { get; set; }
    public User? User { get; set; }
    [ForeignKey("GameId"), Required]
    public int? GameId { get; set; }
    public Game? Game { get; set; }
}

