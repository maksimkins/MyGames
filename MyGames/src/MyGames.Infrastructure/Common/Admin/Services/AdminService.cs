#pragma warning disable CS1998

using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using MyGames.Core.Common.Admin.Services;
using MyGames.Core.Role.Enums;
using MyGames.Core.Role.Models;
using MyGames.Core.User.Models;
using MyGames.Infrastructure.Data.DbContext;

namespace MyGames.Infrastructure.Common.Admin.Services;


public class AdminService : IAdminService
{
    private readonly UserManager<User> _userManager;

    private readonly RoleManager<Role> _roleManager;

    private readonly MyGamesDbContext _context;

    public AdminService(UserManager<User> userManager, RoleManager<Role> roleManager, MyGamesDbContext context)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _context = context;
    }
    public async Task<IdentityResult> AssignRoleToUserAsync(int userId, UserRoles role)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString()) ?? throw new ArgumentException($"cannot find user with id: {userId}");
        var roleName = role.ToString();

        if (!await _roleManager.RoleExistsAsync(roleName))
            return IdentityResult.Failed(new IdentityError { Description = $"Role {roleName} not found." });

        return await _userManager.AddToRoleAsync(user, roleName);
    }

    public async Task<IEnumerable<User>> GetUsersAsync(int PageNumber, int PageSize)
    {
        var users =  _context.Users.Skip((PageNumber - 1) * PageSize).Take(PageSize).ToList(); 
        return users;
    }

    public async Task<User> GetUserByUsernameAsync(string username)
    {
        var user = await _userManager.FindByNameAsync(username) ?? throw new ArgumentException($"cannot find user with username: {username}");

        return user;
    }

    public async Task<IEnumerable<string>> GetRolesByUsernameAsync(string username)
    {
        var user  = await GetUserByUsernameAsync(username: username);
        return await _userManager.GetRolesAsync(user);
    }

    public async Task<User> GetUserByIdAsync(int userId)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString()) ?? throw new ArgumentException($"cannot find user with id: {userId}");

        return user;
    }

    public async Task<int> GetUsersCount()
    {
        return _context.Users.Count();
    }

    public async Task<IdentityResult> RemoveRoleFromUserAsync(int userId, UserRoles role)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString()) ?? throw new ArgumentException($"cannot find user with id: {userId}");
        var roleName = role.ToString();

        if (!await _roleManager.RoleExistsAsync(roleName))
            return IdentityResult.Failed(new IdentityError { Description = $"Role {roleName} not found." });
        
        var rolesBeforeRemoval = await _userManager.GetRolesAsync(user);

        if(rolesBeforeRemoval.Count == 1 && rolesBeforeRemoval.First() == UserRoles.User.ToString())
        {
            return IdentityResult.Failed(new IdentityError { Description = $"You cannot delete default role." });
        }

        var removeResult = await _userManager.RemoveFromRoleAsync(user, roleName);

        var rolesAfterRemoval = await _userManager.GetRolesAsync(user);

        if(rolesAfterRemoval.Count == 0)
        {
            _ = await _userManager.AddToRoleAsync(user, UserRoles.User.ToString());
        }

        return removeResult;
    }


    public async Task<IdentityResult> ToggleBanUser(int userId)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());

        if (user is null)
            throw new ArgumentException("User not found!");

        user.IsBanned = !user.IsBanned;
        return await _userManager.UpdateAsync(user);
    }

    public async Task<IdentityResult> ToggleMuteUser(int userId)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString()) ?? throw new ArgumentException($"cannot find user with id: {userId}");

        user.IsMuted = !user.IsMuted;
        await _userManager.UpdateAsync(user);

        var claims = await _userManager.GetClaimsAsync(user) ?? throw new ArgumentException($"cannot find calims of user with id: {user.Id}");

        var isMutedClaim = claims.FirstOrDefault(c => c.Type == "IsMuted") ?? throw new ArgumentException("Muted claim doesn't exist!");


        return await _userManager.ReplaceClaimAsync(user, isMutedClaim, new Claim("IsMuted", user.IsMuted.ToString()));
    }
}
