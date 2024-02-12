using HouYun3.IRepositories;
using HouYun3.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HouYun3.Controllers
{
   public class WatchHistoryController : Controller
    {
        private readonly IWatchHistoryRepository _watchHistoryRepository;
        private readonly IVideoRepository _videoRepository;
        private readonly IUserRepository _userRepository;
        private readonly IChannelRepository _channelRepository;

        public WatchHistoryController(IWatchHistoryRepository watchHistoryRepository, IVideoRepository videoRepository, IUserRepository userRepository, IChannelRepository channelRepository)
        {
            _watchHistoryRepository = watchHistoryRepository;
            _videoRepository = videoRepository;
            _userRepository = userRepository;
            _channelRepository = channelRepository;
            _channelRepository = channelRepository;
        }

       /* public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var watchHistoryItems = await _watchHistoryRepository.GetWatchHistoryByUserId(userId);
            return View(watchHistoryItems);
        }*/

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddToWatchHistory(Guid videoId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var channelId = await _channelRepository.GetChannelIdByUserId(userId);
            var user = await _channelRepository.GetChannelById(channelId);
            var video = await _videoRepository.GetVideoById(videoId);

            if (user != null && video != null)
            {
                var watchHistory = new WatchHistory
                {
                    Channel = user,
                    Video = video
                };

                await _watchHistoryRepository.AddWatchHistory(watchHistory);
            }

            return RedirectToAction("Index", "Video");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteAllWatchHistoryAsync()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var channelId = await _channelRepository.GetChannelIdByUserId(userId);
            await _watchHistoryRepository.DeleteAllWatchHistory(channelId);

            return RedirectToAction("Index", "WatchHistory");
        }


    }
}
