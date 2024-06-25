using MyGames.Services;
using Moq;
using MyGames.Repositories;
using MyGames.Models;
using MyGames.Repositories.Base;
using Xunit.Sdk;

namespace MyGames.Tests.Services
{
    public class CommentServiceTests
    {
        [Theory]
        [InlineData(1, null, "text", "title")]
        [InlineData(null, 1, "text", "title")]
        [InlineData(1, 1, null, "title")]
        [InlineData(1, 1, "text", null)]
        public async Task CreateUserAsync_UserContainsNullParams_ThrowsArgumentNullException(int? UserId, int? GameId, string? Text, string? Title)
        {
            Comment commentToTest = new Comment()
            {
                UserId = UserId,
                GameId = GameId,
                Text = Text,
                Title = Title,
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
                Title = "Title",
            };
            Mock<ICommentRepository> commentRepositoryMock = new Mock<ICommentRepository>();

            commentRepositoryMock.Setup((repo) => repo.CreateAsync(commentToTest));
        

            var commentService = new CommentService(
                repository: commentRepositoryMock.Object);

            await commentService.CreateCommentAsync(commentToTest);
        }
    }
}