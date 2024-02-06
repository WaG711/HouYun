namespace HouYun3.Models
{
    public class VideoDetailsViewModel
    {
        public Video Video { get; set; }
        public IEnumerable<Comment> Comments { get; set; }
        public int LikesCount { get; set; }
        public bool IsLikedByCurrentUser { get; set; }
    }
}
