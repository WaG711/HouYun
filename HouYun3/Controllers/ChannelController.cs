using HouYun3.IRepositories;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HouYun3.Controllers
{
    public class ChannelController : Controller
    {
        private readonly IChannelRepository _channelRepository;

        public ChannelController(IChannelRepository channelRepository, IVideoRepository videoRepository)
        {
            _channelRepository = channelRepository;
        }

        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var channel = await _channelRepository.GetChannelByUserId(userId);

            return View(channel);
        }
    }
}
