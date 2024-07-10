using Moq;

namespace MyGames.Infrastructure.Tests.Comment.Services;

using MyGames.Core.Comment.Models;
using MyGames.Core.Comment.Repositories.Base;
using MyGames.Infrastructure.Comment.Services;

public class CommentServiceTests
{
    public CommentServiceTests()
    {

    }

    [Theory]
    [InlineData(1, null, "text", 4)]
    [InlineData(null, 1, "text", 4)]
    [InlineData(1, 1, null, 4)]
    public async Task CreateUserAsync_UserContainsNullParams_ThrowsArgumentNullException(int? UserId, int? GameId, string? Text, float Rate)
    {
        Comment commentToTest = new Comment()
        {
            UserId = UserId,
            GameId = GameId,
            Text = Text,
            Rate = Rate,
        };
        Mock<ICommentRepository> commentRepositoryMock = new Mock<ICommentRepository>();

        commentRepositoryMock.Setup((repo) => repo.CreateAsync(commentToTest));
    

        var commentService = new CommentService(
            repository: commentRepositoryMock.Object);

        await Assert.ThrowsAsync<ArgumentNullException>(async () => await commentService.CreateCommentAsync(commentToTest));
    }

    [Fact]
    public async Task CreateUserAsync_ProperParams_DoesntThrowException()
    {
        Comment commentToTest = new Comment()
        {
            UserId = 1,
            GameId = 1,
            Text = "Text",
            Rate = 4,
        };
        Mock<ICommentRepository> commentRepositoryMock = new Mock<ICommentRepository>();

        commentRepositoryMock.Setup((repo) => repo.CreateAsync(commentToTest));
    

        var commentService = new CommentService(
            repository: commentRepositoryMock.Object);

        await commentService.CreateCommentAsync(commentToTest);
    }
}
