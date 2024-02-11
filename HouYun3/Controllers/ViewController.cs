using HouYun3.IRepositories;
using HouYun3.Models;
using HouYun3.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HouYun3.Controllers
{
    public class ViewController : Controller
    {
        private readonly IViewRepository _viewRepository;
        private readonly IChannelRepository _channelRepository;

        public ViewController(IViewRepository viewRepository, IChannelRepository channelRepository)
        {
            _viewRepository = viewRepository;
            _channelRepository = channelRepository;
        }

        [HttpPost]
        [Route("ViewController/AddView/{videoId}")]
        public async Task<IActionResult> AddView(Guid videoId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var channelId = await _channelRepository.GetChannelIdByUserId(userId);

            var existingView = await _viewRepository.GetViewByVideoAndChannel(videoId, channelId);
            if (existingView != null)
            {
                return Ok();
            }

            var view = new View
            {
                VideoId = videoId,
                ChannelId = channelId
            };

            await _viewRepository.AddView(view);

            return Ok();
        }
    }
}
