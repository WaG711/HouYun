using HouYun3.IRepositories;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HouYun3.Controllers
{
    public class ChannelController : Controller
    {
        private readonly IChannelRepository _channelRepository;

        public ChannelController(IChannelRepository channelRepository)
        {
            _channelRepository = channelRepository;
        }

        public async Task<IActionResult> Index(string channelName)
        {
            var channel = await _channelRepository.GetChannelByName(channelName);

            if (channel == null)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                channel = await _channelRepository.GetChannelByUserId(userId);
            }

            return View(channel);
        }
    }
}
