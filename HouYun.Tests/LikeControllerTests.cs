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
    public class LikeControllerTests
    {
        private readonly string _userId = "testUserId";
        private readonly Guid _videoId = Guid.NewGuid();
        private readonly Guid _channelId = Guid.NewGuid();

        [Fact]
        public async Task Index_ReturnsViewWithLikedVideos()
        {
            var expectedLikedVideos = new List<Video>();

            var mockLikeRepository = new Mock<ILikeRepository>();
            var mockChannelRepository = new Mock<IChannelRepository>();

            mockLikeRepository.Setup(repo => repo.GetChannelLikedVideos(_channelId)).ReturnsAsync(expectedLikedVideos);
            mockChannelRepository.Setup(repo => repo.GetChannelIdByUserId(_userId)).ReturnsAsync(_channelId);

            var controller = CreateLikeController(mockLikeRepository.Object, mockChannelRepository.Object);

            var result = await controller.Index();

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<List<Video>>(viewResult.ViewData.Model);
            Assert.Equal(expectedLikedVideos, model);
        }

        [Fact]
        public async Task AddLike_WithNewLike_ReturnsOkResult()
        {
            var mockLikeRepository = new Mock<ILikeRepository>();
            mockLikeRepository.Setup(repo => repo.GetLikeByChannelIdAndVideoId(_channelId, _videoId))
                .ReturnsAsync((Like)null);

            mockLikeRepository.Setup(repo => repo.AddLike(It.IsAny<Like>()))
                .Returns(Task.FromResult(new Like()));

            var mockChannelRepository = new Mock<IChannelRepository>();
            mockChannelRepository.Setup(repo => repo.GetChannelIdByUserId(_userId))
                .ReturnsAsync(_channelId);

            var controller = CreateLikeController(mockLikeRepository.Object, mockChannelRepository.Object);

            var result = await controller.AddLike(_videoId) as OkResult;

            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);

            mockLikeRepository.Verify(repo => repo.GetLikeByChannelIdAndVideoId(_channelId, _videoId), Times.Once);
            mockLikeRepository.Verify(repo => repo.AddLike(It.IsAny<Like>()), Times.Once);
        }

        [Fact]
        public async Task RemoveLike_WithExistingLike_ReturnsOkResult()
        {
            var likeId = Guid.NewGuid();

            var mockLikeRepository = new Mock<ILikeRepository>();
            mockLikeRepository.Setup(repo => repo.GetLikeByChannelIdAndVideoId(_channelId, _videoId))
                .ReturnsAsync(new Like { LikeId = likeId });
            mockLikeRepository.Setup(repo => repo.DeleteLike(likeId))
                .Returns(Task.CompletedTask);

            var mockChannelRepository = new Mock<IChannelRepository>();
            mockChannelRepository.Setup(repo => repo.GetChannelIdByUserId(_userId))
                .ReturnsAsync(_channelId);

            var controller = CreateLikeController(mockLikeRepository.Object, mockChannelRepository.Object);
 
            var result = await controller.RemoveLike(_videoId) as OkResult;

            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);

            mockLikeRepository.Verify(repo => repo.GetLikeByChannelIdAndVideoId(_channelId, _videoId), Times.Once);
            mockLikeRepository.Verify(repo => repo.DeleteLike(likeId), Times.Once);
        }

        [Fact]
        public async Task RemoveLike_WithNoExistingLike_ReturnsOkResult()
        {
            var mockLikeRepository = new Mock<ILikeRepository>();
            mockLikeRepository.Setup(repo => repo.GetLikeByChannelIdAndVideoId(_channelId, _videoId))
                .ReturnsAsync((Like)null);

            var mockChannelRepository = new Mock<IChannelRepository>();
            mockChannelRepository.Setup(repo => repo.GetChannelIdByUserId(_userId))
                .ReturnsAsync(_channelId);

            var controller = CreateLikeController(mockLikeRepository.Object, mockChannelRepository.Object);

            var result = await controller.RemoveLike(_videoId) as OkResult;

            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);

            mockLikeRepository.Verify(repo => repo.GetLikeByChannelIdAndVideoId(_channelId, _videoId), Times.Once);
            mockLikeRepository.Verify(repo => repo.DeleteLike(It.IsAny<Guid>()), Times.Never);
        }

        private LikeController CreateLikeController(ILikeRepository likeRepository, IChannelRepository channelRepository)
        {
            var controller = new LikeController(likeRepository, channelRepository);
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
