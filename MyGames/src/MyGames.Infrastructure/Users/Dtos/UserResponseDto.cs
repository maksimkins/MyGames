
using MyGames.Core.User.Models;

namespace MyGames.Infrastructure.Users.Dtos;

public class UserResponseDto
{
    public required User User { get; set; }
    public required ICollection<string> Roles { get; set; }
}