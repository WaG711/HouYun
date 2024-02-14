using HouYun3.IRepositories;
using HouYun3.Repositories;
using Microsoft.AspNetCore.Http;
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
            return View(notifys);
        }
    }
}
