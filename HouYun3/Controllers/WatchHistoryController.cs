using HouYun3.IRepositories;
using HouYun3.Models;
using HouYun3.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HouYun3.Controllers
{
    public class WatchHistoryController : Controller
    {
        private readonly IWatchHistoryRepository _watchHistoryRepository;
        private readonly IVideoRepository _videoRepository;
        private readonly IUserRepository _userRepository;

        public WatchHistoryController(IWatchHistoryRepository watchHistoryRepository, IVideoRepository videoRepository, IUserRepository userRepository)
        {
            _watchHistoryRepository = watchHistoryRepository;
            _videoRepository = videoRepository;
            _userRepository = userRepository;
        }

        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var watchLaterList = await _watchHistoryRepository.GetWatchHistoryByIdAsync(int.Parse(userId));

            return View(watchLaterList);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddToWatchHistory(int videoId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userRepository.GetUserByIdAsync(userId.ToString());
            var video = await _videoRepository.GetVideoByIdAsync(videoId);

            if (user != null && video != null)
            {
                var watchHistory = new WatchHistory
                {
                    User = user,
                    Video = video
                };

                await _watchHistoryRepository.AddWatchHistoryAsync(watchHistory);
            }

            return RedirectToAction("Index", "Video");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteAllWatchHistoryAsync()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _watchHistoryRepository.DeleteAllWatchHistoryAsync(userId);

            return RedirectToAction("Index");
        }

    }
}
