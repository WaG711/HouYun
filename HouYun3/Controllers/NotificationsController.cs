using HouYun3.IRepositories;
using HouYun3.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HouYun3.Controllers
{
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
            var notifys = await _notificationRepository.GetAllNotificationsByChannelId(channelId);

            await Update(notifys);

            return View(notifys);
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
