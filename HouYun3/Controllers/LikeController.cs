using HouYun3.IRepositories;
using HouYun3.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HouYun3.Controllers
{
    public class LikeController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IVideoRepository _videoRepository;
        private readonly ILikeRepository _likeRepository;

        public LikeController(IUserRepository userRepository, IVideoRepository videoRepository, ILikeRepository likeRepository)
        {
            _userRepository = userRepository;
            _videoRepository = videoRepository;
            _likeRepository = likeRepository;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddLike(Guid videoId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userRepository.GetUserById(userId);
            var video = await _videoRepository.GetVideoById(videoId);

            if (user != null && video != null)
            {
                var existingLike = await _likeRepository.GetLikeByUserIdAndVideoId(userId, videoId);
                if (existingLike == null)
                {
                    var like = new Like
                    {
                        User = user,
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
            var video = await _videoRepository.GetVideoById(videoId);

            if (userId != null && video != null)
            {
                var likeToRemove = await _likeRepository.GetLikeByUserIdAndVideoId(userId, videoId);

                if (likeToRemove != null)
                {
                    await _likeRepository.DeleteLike(likeToRemove.LikeId);
                }
            }

            return RedirectToAction("Details", "Video", new { id = videoId });
        }
    }
}
