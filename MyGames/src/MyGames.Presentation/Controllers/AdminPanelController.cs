
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyGames.Core.Common.Admin.Services;
using MyGames.Core.Role.Enums;
using MyGames.Infrastructure.Users.Dtos;


namespace MyGames.Presentation.Controllers;

[Authorize("MyPolicy")]
[Route("/api/[controller]/[action]")]
public class AdminPanelController : ControllerBase
{
    private readonly IAdminService _adminService;

    public AdminPanelController(IAdminService adminService)
    {
        _adminService = adminService;
    }


    // [Authorize(Roles = "Admin")]
    [HttpGet]
    public async Task<IActionResult> GetUsersCount()
    {
        try
        {
            var usersCount = await _adminService.GetUsersCount();

            return Ok(new {
                usersCount = usersCount
            });
        }
        catch(ArgumentException exception)
        {   
            return BadRequest(exception.Message);
        }
        catch(Exception exception)
        {
            return this.StatusCode(500, exception.Message);
        }
    }

    [HttpPost]
    // [Authorize(Roles = "Admin")]
    public async Task<IActionResult> AssignRoleAsync([FromQuery] int userId, [FromQuery] UserRoles role)
    {
        try
        {
            var result = await _adminService.AssignRoleToUserAsync(userId, role);
            return result.Succeeded ? Ok() : BadRequest(result.Errors);
        }
        catch(ArgumentException exception)
        {   
            return BadRequest(exception.Message);
        }
        catch(Exception exception)
        {
            return this.StatusCode(500, exception.Message);
        }
    }

    [HttpPost]
    // [Authorize(Roles = "Admin")]
    public async Task<IActionResult> RemoveRoleAsync([FromQuery] int userId, [FromQuery] UserRoles role)
    {
        try
        {
            var result = await _adminService.RemoveRoleFromUserAsync(userId: userId, role);
            return result.Succeeded ? Ok() : BadRequest(result.Errors);
        }
        catch(ArgumentException exception)
        {   
            return BadRequest(exception.Message);
        }
        catch(Exception exception)
        {
            return this.StatusCode(500, exception.Message);
        }
    }

    [HttpPost("{userId}")]
    // [Authorize(Roles = "Admin")]
    public async Task<IActionResult> ToggleMuteAsync(int userId)
    {
        try
        {
            var result = await _adminService.ToggleMuteUser(userId);
            return result.Succeeded ? Ok() : BadRequest(result.Errors);
        }
        catch(ArgumentException exception)
        {   
            return BadRequest(exception.Message);
        }
        catch(Exception exception)
        {
            return this.StatusCode(500, exception.Message);
        }
    }

    [HttpPost("{userId}")]
    // [Authorize(Roles = "Admin")]
    public async Task<IActionResult> ToggleBanAsync(int userId)
    {
        try
        {
            var result = await _adminService.ToggleBanUser(userId);
            return result.Succeeded ? Ok() : BadRequest(result.Errors);
        }
        catch(ArgumentException exception)
        {   
            return BadRequest(exception.Message);
        }
        catch(Exception exception)
        {
            return this.StatusCode(500, exception.Message);
        }
    }

    [HttpGet("/api/[controller]/User/{userId}")]
    // [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetUserInfoAsync(int userId)
    {
        try
        {
            var user = await _adminService.GetUserByIdAsync(userId);
            return Ok(user);
        }
        catch(ArgumentException exception)
        {   
            return BadRequest(exception.Message);
        }
        catch(Exception exception)
        {
            return this.StatusCode(500, exception.Message);
        }
    }

    [HttpGet("/api/[controller]/User")]
    // [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetUsersAsync([FromQuery]int PageNumber, [FromQuery]int PageSize)
    {
        try
        {
            var users = await _adminService.GetUsersAsync(PageNumber, PageSize);

            var userDtos = new List<UserResponseDto>();

            foreach (var user in users)
            {
                var roles = await _adminService.GetRolesByUsernameAsync(user.UserName!);

                var userDto = new UserResponseDto()
                {
                    User = user,
                    Roles = roles.ToList()
                };

                userDtos.Add(userDto);
            }

            return Ok(userDtos);
        }
        catch(Exception exception)
        {
            return this.StatusCode(500, exception.Message);
        }
    }
}
