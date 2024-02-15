using HouYun.IRepositories;
using HouYun.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HouYun.Controllers
{
    [Authorize(Roles = "Admin")]
    public class LikeController : Controller
    {
        private readonly ILikeRepository _likeRepository;
        private readonly IChannelRepository _channelRepository;

        public LikeController(ILikeRepository likeRepository, IChannelRepository channelRepository)
        {
            _likeRepository = likeRepository;
            _channelRepository = channelRepository;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddLike(Guid videoId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var channelId = await _channelRepository.GetChannelIdByUserId(userId);

            var existingLike = await _likeRepository.GetLikeByChannelIdAndVideoId(channelId, videoId);
            if (existingLike == null)
            {
                var like = new Like
                {
                    ChannelId = channelId,
                    VideoId = videoId
                };

                await _likeRepository.AddLike(like);
            }

            return Ok();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveLike(Guid videoId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var channelId = await _channelRepository.GetChannelIdByUserId(userId);

            var likeToRemove = await _likeRepository.GetLikeByChannelIdAndVideoId(channelId, videoId);

            if (likeToRemove != null)
            {
                await _likeRepository.DeleteLike(likeToRemove.LikeId);
            }

            return Ok();
        }
    }
}
