using HouYun3.IRepositories;
using HouYun3.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HouYun3.Controllers
{
    public class LikeController : Controller
    {
        private readonly IVideoRepository _videoRepository;
        private readonly ILikeRepository _likeRepository;
        private readonly IChannelRepository _channelRepository;

        public LikeController(IVideoRepository videoRepository, ILikeRepository likeRepository, IChannelRepository channelRepository)
        {
            _videoRepository = videoRepository;
            _likeRepository = likeRepository;
            _channelRepository = channelRepository;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddLike(Guid videoId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var channel = await _channelRepository.GetChannelByUserId(userId);
            var channelId = await _channelRepository.GetChannelIdByUserId(userId);
            var video = await _videoRepository.GetVideoById(videoId);

            if (channel != null && video != null)
            {
                var existingLike = await _likeRepository.GetLikeByChannelIdAndVideoId(channelId, videoId);
                if (existingLike == null)
                {
                    var like = new Like
                    {
                        Channel = channel,
                        Video = video
                    };

                    await _likeRepository.AddLike(like);
                }
            }

            return RedirectToAction("Details", "Video", new { id = videoId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveLike(Guid videoId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var channelId = await _channelRepository.GetChannelIdByUserId(userId);
            var video = await _videoRepository.GetVideoById(videoId);

            if (userId != null && video != null)
            {
                var likeToRemove = await _likeRepository.GetLikeByChannelIdAndVideoId(channelId, videoId);

                if (likeToRemove != null)
                {
                    await _likeRepository.DeleteLike(likeToRemove.LikeId);
                }
            }

            return RedirectToAction("Details", "Video", new { id = videoId });
        }
    }
}
