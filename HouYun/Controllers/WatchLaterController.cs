using HouYun.IRepositories;
using HouYun.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HouYun.Controllers
{
    [Authorize(Roles = "Admin,User")]
    public class WatchLaterController : Controller
    {
        private readonly IWatchLaterRepository _watchLaterRepository;
        private readonly IChannelRepository _channelRepository;

        public WatchLaterController(IWatchLaterRepository watchLaterRepository, IChannelRepository channelRepository)
        {
            _watchLaterRepository = watchLaterRepository;
            _channelRepository = channelRepository;
        }

        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var channelId = await _channelRepository.GetChannelIdByUserId(userId);

            var watchLaterItems = await _watchLaterRepository.GetVideosByChannelId(channelId);
            return View(watchLaterItems);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddToWatchLater(Guid videoId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var channelId = await _channelRepository.GetChannelIdByUserId(userId);

            var existingItem = await _watchLaterRepository.GetWatchLaterItemByChannelIdAndVideoId(channelId, videoId);
            if (existingItem != null)
            {
                return Ok();
            }

            var watchLaterItem = new WatchLater
            {
                ChannelId = channelId,
                VideoId = videoId
            };

            await _watchLaterRepository.AddWatchLaterItem(watchLaterItem);

            return Ok();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _watchLaterRepository.DeleteWatchLaterItem(id);
            return RedirectToAction("Index");
        }
    }
}