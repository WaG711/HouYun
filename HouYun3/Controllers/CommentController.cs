using HouYun3.IRepositories;
using HouYun3.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HouYun3.Controllers
{
    public class CommentController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IVideoRepository _videoRepository;
        private readonly ICommentRepository _commentRepository;

        public CommentController(IUserRepository userRepository, IVideoRepository videoRepository, ICommentRepository commentRepository)
        {
            _userRepository = userRepository;
            _videoRepository = videoRepository;
            _commentRepository = commentRepository;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddComment(int videoId, string commentText)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userRepository.GetUserById(userId);
            var video = await _videoRepository.GetVideoById(videoId);

            if (user != null && video != null && !string.IsNullOrWhiteSpace(commentText))
            {
                var comment = new Comment
                {
                    Text = commentText,
                    User = user,
                    Video = video
                };

                await _commentRepository.AddComment(comment);
            }

            return RedirectToAction("Details", new { id = videoId });
        }
    }
}
