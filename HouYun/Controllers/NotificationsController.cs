using HouYun.IRepositories;
using HouYun.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HouYun.Controllers
{
    [Authorize(Roles = "Admin,User")]
    public class NotificationsController : Controller
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly IChannelRepository _channelRepository;

        public NotificationsController(INotificationRepository notificationRepository, IChannelRepository channelRepository)
        {
            _notificationRepository = notificationRepository;
            _channelRepository = channelRepository;

        }

        public async Task<ActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var channelId = await _channelRepository.GetChannelIdByUserId(userId);

            var notifications = await _notificationRepository.GetAllNotificationsByChannelId(channelId);
            await Update(notifications);


            return PartialView("_NotificationsPartial", notifications);
        }

        private async Task<ActionResult> Update(IEnumerable<Notification> notifications)
        {
            foreach (var notification in notifications)
            {
                await _notificationRepository.UpdateNotification(notification);
            }

            await DeleteNotifications();

            return Ok();
        }

        private async Task<ActionResult> DeleteNotifications()
        {
            await _notificationRepository.DeleteReadNotifications();

            return Ok();
        }
    }
}
