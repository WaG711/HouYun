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
    public class WatchLaterControllerTests
    {
        private readonly string _userId = "testUserId";
        private readonly Guid _channelId = Guid.NewGuid();

        [Fact]
        public async Task Index_ReturnsViewWithWatchLaterItems()
        {
            var watchLaterItems = new List<Video>
            {
                new Video(),
                new Video()
            };

            var mockWatchLaterRepository = new Mock<IWatchLaterRepository>();
            var mockChannelRepository = new Mock<IChannelRepository>();

            mockChannelRepository.Setup(repo => repo.GetChannelIdByUserId(_userId))
                .ReturnsAsync(_channelId);

            mockWatchLaterRepository.Setup(repo => repo.GetVideosByChannelId(_channelId))
                .ReturnsAsync(watchLaterItems);

            var controller = CreateWatchLaterController(mockWatchLaterRepository.Object, mockChannelRepository.Object);

            var result = await controller.Index() as ViewResult;

            Assert.NotNull(result);
            Assert.Equal(watchLaterItems, result.Model);
        }

        [Fact]
        public async Task AddToWatchLater_ReturnsOkResult_WhenWatchLaterItemIsAdded()
        {
            var videoId = Guid.NewGuid();

            var mockWatchLaterRepository = new Mock<IWatchLaterRepository>();
            var mockChannelRepository = new Mock<IChannelRepository>();

            mockChannelRepository.Setup(repo => repo.GetChannelIdByUserId(_userId))
                .ReturnsAsync(_channelId);

            mockWatchLaterRepository.Setup(repo => repo.GetWatchLaterItemByChannelIdAndVideoId(_channelId, videoId))
                .ReturnsAsync((WatchLater)null);

            var controller = CreateWatchLaterController(mockWatchLaterRepository.Object, mockChannelRepository.Object);

            var result = await controller.AddToWatchLater(videoId) as OkResult;

            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
        }

        [Fact]
        public async Task AddToWatchLater_ReturnsOkResult_WhenWatchLaterItemExists()
        {
            var videoId = Guid.NewGuid();

            var existingWatchLaterItem = new WatchLater { ChannelId = _channelId, VideoId = videoId };

            var mockWatchLaterRepository = new Mock<IWatchLaterRepository>();
            var mockChannelRepository = new Mock<IChannelRepository>();

            mockChannelRepository.Setup(repo => repo.GetChannelIdByUserId(_userId))
                .ReturnsAsync(_channelId);

            mockWatchLaterRepository.Setup(repo => repo.GetWatchLaterItemByChannelIdAndVideoId(_channelId, videoId))
                .ReturnsAsync(existingWatchLaterItem);

            var controller = CreateWatchLaterController(mockWatchLaterRepository.Object, mockChannelRepository.Object);

            var result = await controller.AddToWatchLater(videoId) as OkResult;

            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
        }

        [Fact]
        public async Task Delete_ReturnsRedirectToActionResult_WhenWatchLaterItemIsDeleted()
        {
            var mockWatchLaterRepository = new Mock<IWatchLaterRepository>();

            var controller = new WatchLaterController(mockWatchLaterRepository.Object, null);

            var result = await controller.Delete(Guid.NewGuid()) as RedirectToActionResult;

            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName);
        }

        private WatchLaterController CreateWatchLaterController(IWatchLaterRepository watchLaterRepository, IChannelRepository channelRepository)
        {
            var controller = new WatchLaterController(watchLaterRepository, channelRepository);
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
