namespace MyGames.Core.Common.Admin.Services;

using Microsoft.AspNetCore.Identity;
using MyGames.Core.Role.Enums;
using MyGames.Core.User.Models;

public interface IAdminService
{
    Task<IdentityResult> AssignRoleToUserAsync(int userId, UserRoles role);

    Task<IEnumerable<User>> GetUsersAsync(int PageNumber, int PageSize);

    Task<User> GetUserByUsernameAsync(string username);

    Task<IEnumerable<string>> GetRolesByUsernameAsync(string username);

    Task<User> GetUserByIdAsync(int userId);

    Task<int> GetUsersCount();

    Task<IdentityResult> RemoveRoleFromUserAsync(int userId, UserRoles role);

    Task<IdentityResult> ToggleBanUser(int userId);

    Task<IdentityResult> ToggleMuteUser(int userId);




}
