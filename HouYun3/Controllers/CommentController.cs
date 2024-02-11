using HouYun3.IRepositories;
using HouYun3.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HouYun3.Controllers
{
    public class CommentController : Controller
    {
        private readonly IChannelRepository _channelRepository;
        private readonly IVideoRepository _videoRepository;
        private readonly ICommentRepository _commentRepository;

        public CommentController(IVideoRepository videoRepository, ICommentRepository commentRepository, IChannelRepository channelRepository)
        {
            _videoRepository = videoRepository;
            _commentRepository = commentRepository;
            _channelRepository = channelRepository;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddComment(Guid videoId, string commentText)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var channel = await _channelRepository.GetChannelByUserId(userId);
            var video = await _videoRepository.GetVideoById(videoId);

            if (channel != null && video != null && !string.IsNullOrWhiteSpace(commentText))
            {
                var comment = new Comment
                {
                    Text = commentText,
                    Channel = channel,
                    Video = video
                };

                await _commentRepository.AddComment(comment);
            }

            return RedirectToAction("Details", "Video", new { id = videoId });
        }
    }
}
