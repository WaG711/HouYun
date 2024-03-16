using HouYun.Controllers;
using HouYun.IRepositories;
using HouYun.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Security.Claims;
using Xunit;

namespace HouYun.Tests
{
    public class CommentControllerTests
    {
        private readonly string _userId = "testUserId";
        private readonly Guid _videoId = Guid.NewGuid();

        [Fact]
        public async Task AddComment_WithValidData_ReturnsOkResult()
        {
            var commentText = "Test comment text";
            var channelId = Guid.NewGuid();

            var mockCommentRepository = new Mock<ICommentRepository>();
            mockCommentRepository.Setup(repo => repo.AddComment(It.IsAny<Comment>()))
                .Returns(Task.FromResult(new Comment()));

            var mockChannelRepository = new Mock<IChannelRepository>();
            mockChannelRepository.Setup(repo => repo.GetChannelIdByUserId(_userId))
                .ReturnsAsync(channelId);

            var controller = CreateCommentController(mockCommentRepository.Object, mockChannelRepository.Object);

            var result = await controller.AddComment(_videoId, commentText) as OkResult;

            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);

            mockCommentRepository.Verify(repo => repo.AddComment(It.IsAny<Comment>()), Times.Once);
        }

        [Fact]
        public async Task AddComment_WithInvalidData_ReturnsOkResult()
        {
            var commentText = "";

            var mockCommentRepository = new Mock<ICommentRepository>();
            var mockChannelRepository = new Mock<IChannelRepository>();

            var controller = CreateCommentController(mockCommentRepository.Object, mockChannelRepository.Object);

            var result = await controller.AddComment(_videoId, commentText) as OkResult;

            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);

            mockCommentRepository.Verify(repo => repo.AddComment(It.IsAny<Comment>()), Times.Never);
        }

        private CommentController CreateCommentController(ICommentRepository commentRepository, IChannelRepository channelRepository)
        {
            var controller = new CommentController(commentRepository, channelRepository);
            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, _userId)
                    }))
                }
            };
            return controller;
        }
    }
}
