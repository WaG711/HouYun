using HouYun.IRepositories;
using HouYun.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HouYun.Controllers
{
   public class WatchHistoryController : Controller
    {
        private readonly IWatchHistoryRepository _watchHistoryRepository;
        private readonly IChannelRepository _channelRepository;

        public WatchHistoryController(IWatchHistoryRepository watchHistoryRepository, IChannelRepository channelRepository)
        {
            _watchHistoryRepository = watchHistoryRepository;
            _channelRepository = channelRepository;
        }

        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var channelId = await _channelRepository.GetChannelIdByUserId(userId);
            var watchHistoryItems = await _watchHistoryRepository.GetWatchHistoryByChannelId(channelId);

            return View(watchHistoryItems);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("WatchHistory/AddToWatchHistory/{videoId}")]
        public async Task<IActionResult> AddToWatchHistory(Guid videoId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var channelId = await _channelRepository.GetChannelIdByUserId(userId);

            var existingWatchHistory = await _watchHistoryRepository.GetWatchHistoryByChannelIdAndVideoId(channelId, videoId);
            if (existingWatchHistory != null)
            {
                existingWatchHistory.WatchDate = DateTime.UtcNow;
                await _watchHistoryRepository.UpdateWatchHistory(existingWatchHistory);
            }
            else
            {
                var watchHistory = new WatchHistory
                {
                    ChannelId = channelId,
                    VideoId = videoId
                };

                await _watchHistoryRepository.AddWatchHistory(watchHistory);
            }

            return Ok();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteAllWatchHistory()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var channelId = await _channelRepository.GetChannelIdByUserId(userId);

            await _watchHistoryRepository.DeleteAllWatchHistory(channelId);

            return RedirectToAction("Index");
        }
    }
}
