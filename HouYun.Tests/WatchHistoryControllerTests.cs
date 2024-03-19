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
    public class WatchHistoryControllerTests
    {
        private readonly string _userId = "testUserId";
        private readonly Guid _channelId = Guid.NewGuid();

        [Fact]
        public async Task Index_ReturnsViewWithWatchHistoryItems()
        {
            var watchHistoryItems = new List<Video> { new Video(), new Video() };

            var mockWatchHistoryRepository = new Mock<IWatchHistoryRepository>();
            var mockChannelRepository = new Mock<IChannelRepository>();

            mockChannelRepository.Setup(repo => repo.GetChannelIdByUserId(_userId))
                .ReturnsAsync(_channelId);

            mockWatchHistoryRepository.Setup(repo => repo.GetWatchHistoryByChannelId(_channelId))
                .ReturnsAsync(watchHistoryItems);

            var controller = CreateWatchHistoryController(mockWatchHistoryRepository.Object, mockChannelRepository.Object);

            var result = await controller.Index() as ViewResult;

            Assert.NotNull(result);
            Assert.Equal(watchHistoryItems, result.Model);
        }

        [Fact]
        public async Task DeleteAllWatchHistory_ReturnsRedirectToIndex()
        {
            var mockChannelRepository = new Mock<IChannelRepository>();
            var mockWatchHistoryRepository = new Mock<IWatchHistoryRepository>();

            mockChannelRepository.Setup(repo => repo.GetChannelIdByUserId(_userId))
                .ReturnsAsync(_channelId);

            mockWatchHistoryRepository.Setup(repo => repo.DeleteAllWatchHistory(_channelId))
                .Returns(Task.CompletedTask);

            var controller = CreateWatchHistoryController(mockWatchHistoryRepository.Object, mockChannelRepository.Object);

            var result = await controller.DeleteAllWatchHistory() as RedirectToActionResult;

            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName);
        }

        [Fact]
        public async Task AddToWatchHistory_ExistingWatchHistory_ReturnsOk()
        {
            var videoId = Guid.NewGuid();

            var mockChannelRepository = new Mock<IChannelRepository>();
            var mockWatchHistoryRepository = new Mock<IWatchHistoryRepository>();

            mockChannelRepository.Setup(repo => repo.GetChannelIdByUserId(_userId))
                .ReturnsAsync(_channelId);

            mockWatchHistoryRepository.Setup(repo => repo.GetWatchHistoryByChannelIdAndVideoId(_channelId, videoId))
                .ReturnsAsync(new WatchHistory { ChannelId = _channelId, VideoId = videoId });

            var controller = CreateWatchHistoryController(mockWatchHistoryRepository.Object, mockChannelRepository.Object);

            var result = await controller.AddToWatchHistory(videoId) as OkResult;

            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
        }

        private WatchHistoryController CreateWatchHistoryController(IWatchHistoryRepository watchHistoryRepository, IChannelRepository channelRepository)
        {
            var controller = new WatchHistoryController(watchHistoryRepository, channelRepository);
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
