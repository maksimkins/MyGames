using MyGames.Services;
using Moq;
using MyGames.Repositories;
using MyGames.Models;
using MyGames.Repositories.Base;

namespace MyGames.Tests.Services;

public class UserServiceTests
{
    [Fact]
    public async Task GetUserAsync_UserFound_DoesntThrowException()
    {
        Mock<IUserRepository> userRepositoryMock = new Mock<IUserRepository>();

        int userId = 1;
        userRepositoryMock.Setup((repo) => repo.GetByIdAsync(userId))
            .ReturnsAsync(new User() {
                Username = "testik12",
                Id = userId,
            });

        var userService = new UserService(
            repository: userRepositoryMock.Object);

        await userService.GetByIdAsync(userId);
    }

    [Fact]
    public async Task GetUserAsync_NotProperId_ThrowsArgumentNullException()
    {
        Mock<IUserRepository> userRepositoryMock = new Mock<IUserRepository>();

        int userId = -1;
        userRepositoryMock.Setup((repo) => repo.GetByIdAsync(userId))
            .ReturnsAsync(value: null);

        var userService = new UserService(
            repository: userRepositoryMock.Object);
        
        await Assert.ThrowsAsync<ArgumentNullException>(async () => await userService.GetByIdAsync(userId));
    }
}
