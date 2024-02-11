using HouYun3.IRepositories;
using Microsoft.AspNetCore.Mvc;
using HouYun3.ViewModels.forVideo;

namespace HouYun3.Controllers
{
    public class ChannelController : Controller
    {
        private readonly IVideoRepository _videoRepository;

        public ChannelController(IVideoRepository videoRepository)
        {
            _videoRepository = videoRepository;
        }
        public async Task<IActionResult> Index(string channelName)
        {
            var videos = await _videoRepository.GetAllVideosByChannelName(channelName);
            var channelViewModel = new ChannelViewModel
            {
                ChannelName = channelName,
                Videos = videos
            };
            return View(channelViewModel);
        }
    }
}
