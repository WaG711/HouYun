using HouYun3.Models;

namespace HouYun3.IRepositories
{
    public interface ILikeRepository
    {
        Task<int> GetLikesCountByVideoId(int videoId);
        Task<bool> IsUserLikedVideo(int videoId, int userId);
        Task AddLike(Like like);
        Task RemoveLike(int likeId);
    }
}
