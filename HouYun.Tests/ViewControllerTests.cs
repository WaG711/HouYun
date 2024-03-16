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
    public class ViewControllerTests
    {
        private readonly string userId = "testUserId";
        private readonly Guid videoId = Guid.NewGuid();
        private readonly Guid channelId = Guid.NewGuid();

        [Fact]
        public async Task AddView_ReturnsOkResult_WhenViewIsAddedSuccessfully()
        {
            var mockViewRepository = new Mock<IViewRepository>();
            var mockChannelRepository = new Mock<IChannelRepository>();

            var controller = CreateViewController(mockViewRepository.Object, mockChannelRepository.Object);

            mockChannelRepository.Setup(repo => repo.GetChannelIdByUserId(userId))
                .ReturnsAsync(channelId);

            mockViewRepository.Setup(repo => repo.GetViewByVideoAndChannel(videoId, channelId))
                .ReturnsAsync((View)null);

            var result = await controller.AddView(videoId) as OkResult;

            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            mockViewRepository.Verify(repo => repo.AddView(It.IsAny<View>()), Times.Once);
        }

        [Fact]
        public async Task AddView_ReturnsOkResult_WhenViewAlreadyExists()
        {
            var mockViewRepository = new Mock<IViewRepository>();
            var mockChannelRepository = new Mock<IChannelRepository>();

            var controller = CreateViewController(mockViewRepository.Object, mockChannelRepository.Object);

            mockChannelRepository.Setup(repo => repo.GetChannelIdByUserId(userId))
                .ReturnsAsync(channelId);

            mockViewRepository.Setup(repo => repo.GetViewByVideoAndChannel(videoId, channelId))
                .ReturnsAsync(new View());

            var result = await controller.AddView(videoId) as OkResult;

            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            mockViewRepository.Verify(repo => repo.AddView(It.IsAny<View>()), Times.Never);
        }

        private ViewController CreateViewController(IViewRepository viewRepository, IChannelRepository channelRepository)
        {
            var controller = new ViewController(viewRepository, channelRepository);
            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, userId)
                    }))
                }
            };
            return controller;
        }
    }
}
