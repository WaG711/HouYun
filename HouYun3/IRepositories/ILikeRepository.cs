using HouYun3.Models;

namespace HouYun3.IRepositories
{
    public interface ILikeRepository
    {
        Task<Like> GetLikeByIdAsync(int id);
        Task<List<Like>> GetLikesByVideoIdAsync(int videoId);
        Task AddLikeAsync(Like like);
        Task DeleteLikeAsync(int id);
        Task<int> GetLikesCountByVideoIdAsync(int videoId);
        Task<bool> IsUserLikedVideoAsync(int videoId, string userId);
    }
}
