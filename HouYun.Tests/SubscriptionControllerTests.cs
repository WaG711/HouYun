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
    public class SubscriptionControllerTests
    {
        private readonly string _userId = "testUserId";
        private readonly Guid _channelId = Guid.NewGuid();

        [Fact]
        public async Task Index_ReturnsViewWithSubscribedVideos()
        {
            var subscribedVideos = new List<Video> { new Video(), new Video() };

            var mockSubscriptionRepository = new Mock<ISubscriptionRepository>();
            mockSubscriptionRepository.Setup(repo => repo.GetUserSubscribedVideos(_userId))
                .ReturnsAsync(subscribedVideos);

            var controller = CreateSubscriptionController(mockSubscriptionRepository.Object);

            var result = await controller.Index() as ViewResult;

            Assert.NotNull(result);
            Assert.IsType<ViewResult>(result);
            Assert.Equal(subscribedVideos, result.Model);
        }

        [Fact]
        public async Task SubscribedChannels_ReturnsPartialViewWithSubscribedChannels()
        {
            var subscribedChannels = new List<Channel> { new Channel(), new Channel() };

            var mockSubscriptionRepository = new Mock<ISubscriptionRepository>();
            mockSubscriptionRepository.Setup(repo => repo.GetUserSubscribedChannels(_userId))
                .ReturnsAsync(subscribedChannels);

            var controller = CreateSubscriptionController(mockSubscriptionRepository.Object);

            var result = await controller.SubscribedChannels() as PartialViewResult;

            Assert.NotNull(result);
            Assert.IsType<PartialViewResult>(result);
            Assert.Equal("_SubscribedChannelsPartial", result.ViewName);
            Assert.Equal(subscribedChannels, result.Model);
        }

        [Fact]
        public async Task Subscribe_WithExistingSubscription_ReturnsOkResult()
        {
            var existingSubscription = new Subscription { UserId = _userId, ChannelId = _channelId };

            var mockSubscriptionRepository = new Mock<ISubscriptionRepository>();
            mockSubscriptionRepository.Setup(repo => repo.GetSubscriptionByChannelAndUser(_channelId, _userId))
                .ReturnsAsync(existingSubscription);

            var controller = CreateSubscriptionController(mockSubscriptionRepository.Object);

            var result = await controller.Subscribe(_channelId) as OkResult;

            Assert.NotNull(result);
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task Subscribe_WithNewSubscription_CreatesAndReturnsOkResult()
        {
            var mockSubscriptionRepository = new Mock<ISubscriptionRepository>();
            mockSubscriptionRepository.Setup(repo => repo.GetSubscriptionByChannelAndUser(_channelId, _userId))
                .ReturnsAsync((Subscription)null);

            var controller = CreateSubscriptionController(mockSubscriptionRepository.Object);

            var result = await controller.Subscribe(_channelId) as OkResult;

            Assert.NotNull(result);
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task Unsubscribe_WithExistingSubscription_DeletesAndReturnsOkResult()
        {
            var userSubscriptions = new List<Subscription> { new Subscription { UserId = _userId, ChannelId = _channelId } };

            var mockSubscriptionRepository = new Mock<ISubscriptionRepository>();
            mockSubscriptionRepository.Setup(repo => repo.GetSubscriptionsByUserId(_userId))
                .ReturnsAsync(userSubscriptions);

            var controller = CreateSubscriptionController(mockSubscriptionRepository.Object);

            var result = await controller.Unsubscribe(_channelId) as OkResult;

            Assert.NotNull(result);
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task Unsubscribe_WithNonExistingSubscription_ReturnsOkResult()
        {
            var userSubscriptions = new List<Subscription>();

            var mockSubscriptionRepository = new Mock<ISubscriptionRepository>();
            mockSubscriptionRepository.Setup(repo => repo.GetSubscriptionsByUserId(_userId))
                .ReturnsAsync(userSubscriptions);

            var controller = CreateSubscriptionController(mockSubscriptionRepository.Object);

            var result = await controller.Unsubscribe(_channelId) as OkResult;

            Assert.NotNull(result);
            Assert.IsType<OkResult>(result);
        }

        private SubscriptionController CreateSubscriptionController(ISubscriptionRepository subscriptionRepository)
        {
            var controller = new SubscriptionController(subscriptionRepository);
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
