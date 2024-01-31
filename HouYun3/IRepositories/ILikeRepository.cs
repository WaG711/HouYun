using HouYun2.Models;

namespace HouYun2.IRepositories
{
    public interface ILikeRepository
    {
        Task<List<Like>> GetLikes(int videoId);
        Task<Like> GetLike(int likeId);
        Task AddLike(Like like);
        Task DeleteLike(int likeId);
    }
}
