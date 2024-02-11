using HouYun3.IRepositories;
using HouYun3.Models;
using Microsoft.AspNetCore.Mvc;

namespace HouYun3.Controllers
{
    public class ViewController : Controller
    {
        private readonly IViewRepository _viewRepository;

        public ViewController(IViewRepository viewRepository)
        {
            _viewRepository = viewRepository;
        }

        [HttpPost]
        [Route("ViewController/AddView")]
        public async Task<IActionResult> AddView(Guid videoId, Guid channelId)
        {
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
