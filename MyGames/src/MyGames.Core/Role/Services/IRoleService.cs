

using Microsoft.AspNetCore.Identity;
using MyGames.Core.Role.Enums;

namespace MyGames.Core.Role.Services;


public interface IRoleService
{
    Task<IdentityResult> CreateRoleAsync(UserRoles role);

    Task<IdentityResult> DeleteRoleAsync(UserRoles role);

    Task SetupRolesAsync();
}