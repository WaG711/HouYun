using HouYun3.IRepositories;
using HouYun3.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HouYun3.Controllers
{
    public class WatchLaterController : Controller
    {
        private readonly IWatchLaterRepository _watchLaterRepository;
        private readonly IVideoRepository _videoRepository;
        private readonly IUserRepository _userRepository;

        public WatchLaterController(IWatchLaterRepository watchLaterRepository, IVideoRepository videoRepository, IUserRepository userRepository)
        {
            _watchLaterRepository = watchLaterRepository;
            _videoRepository = videoRepository;
            _userRepository = userRepository;
        }

        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var watchLaterList = await _watchLaterRepository.GetWatchLaterByUserId(int.Parse(userId));

            return View(watchLaterList);
        }

        public async Task<IActionResult> Details(int watchLaterId)
        {
            var watchLater = await _watchLaterRepository.GetWatchLaterById(watchLaterId);

            if (watchLater == null)
            {
                return NotFound();
            }

            return RedirectToAction("Details", "Video", new { id = watchLater.Video.VideoId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddToWatchLater(int videoId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userRepository.GetUserByIdAsync(userId.ToString());
            var video = await _videoRepository.GetVideoById(videoId);

            if (user != null && video != null)
            {
                var watchLater = new WatchLater
                {
                    User = user,
                    Video = video
                };

                await _watchLaterRepository.AddWatchLater(watchLater);
            }

            return RedirectToAction("Index", "Video");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveFromWatchLater(int watchLaterId)
        {
            await _watchLaterRepository.DeleteWatchLater(watchLaterId);

            return RedirectToAction("Index");
        }
    }
}
