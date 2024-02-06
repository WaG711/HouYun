using HouYun3.Models;

namespace HouYun3.ViewModels
{
    public class VideoDetailsViewModel
    {
        public Video Video { get; set; }
        public IEnumerable<Comment> Comments { get; set; }
        public int LikesCount { get; set; }
        public bool IsLikedByCurrentUser { get; set; }
    }
}
