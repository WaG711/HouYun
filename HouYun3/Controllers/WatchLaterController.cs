using HouYun3.IRepositories;
using HouYun3.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HouYun3.Controllers
{
    public class WatchLaterController : Controller
    {
        private readonly IWatchLaterRepository _watchLaterRepository;

        public WatchLaterController(IWatchLaterRepository watchLaterRepository)
        {
            _watchLaterRepository = watchLaterRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var watchLaterItems = await _watchLaterRepository.GetVideosByUserId(userId);
            return View(watchLaterItems);
        }

        [HttpGet]
        public async Task<IActionResult> AddToWatchLater(Guid videoId)
        {
            string refererUrl = Request.Headers["Referer"].ToString();

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var existingItem = await _watchLaterRepository.GetWatchLaterItemByUserIdAndVideoId(userId, videoId);
            if (existingItem != null)
            {
                return Redirect(refererUrl);
            }

            var watchLaterItem = new WatchLater
            {
                UserId = userId,
                VideoId = videoId
            };

            await _watchLaterRepository.AddWatchLaterItem(watchLaterItem);

            return Redirect(refererUrl);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _watchLaterRepository.DeleteWatchLaterItem(id);
            return RedirectToAction("Index");
        }
    }
}
