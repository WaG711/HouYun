using HouYun.IRepositories;
using HouYun.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HouYun.Controllers
{
    [Authorize(Roles = "Admin,User,Author")]
    public class CommentController : Controller
    {
        private readonly IChannelRepository _channelRepository;
        private readonly ICommentRepository _commentRepository;

        public CommentController(ICommentRepository commentRepository, IChannelRepository channelRepository)
        {
            _commentRepository = commentRepository;
            _channelRepository = channelRepository;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddComment(Guid videoId, string commentText)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var channelId = await _channelRepository.GetChannelIdByUserId(userId);

            if (!string.IsNullOrWhiteSpace(commentText))
            {
                var comment = new Comment
                {
                    Text = commentText,
                    ChannelId = channelId,
                    VideoId = videoId
                };

                await _commentRepository.AddComment(comment);
            }

            return Ok();
        }
    }
}
