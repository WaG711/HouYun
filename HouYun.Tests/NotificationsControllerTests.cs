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
    public class NotificationsControllerTests
    {
        [Fact]
        public async Task Index_ReturnsPartialViewWithNotifications()
        {
            var userId = "testUserId";
            var channelId = Guid.NewGuid();
            var notifications = new List<Notification> { new Notification(), new Notification() };

            var mockNotificationRepository = new Mock<INotificationRepository>();
            mockNotificationRepository.Setup(repo => repo.GetAllNotificationsByChannelId(channelId))
                .ReturnsAsync(notifications);

            var mockChannelRepository = new Mock<IChannelRepository>();
            mockChannelRepository.Setup(repo => repo.GetChannelIdByUserId(userId))
                .ReturnsAsync(channelId);

            var controller = new NotificationsController(mockNotificationRepository.Object, mockChannelRepository.Object);
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

            var result = await controller.Index() as PartialViewResult;

            Assert.NotNull(result);
            Assert.Equal("_NotificationsPartial", result.ViewName);
            Assert.Equal(notifications, result.Model);
        }
    }
}
