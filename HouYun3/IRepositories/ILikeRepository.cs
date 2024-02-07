using HouYun3.Models;

namespace HouYun3.IRepositories
{
    public interface ILikeRepository
    {
        Task<IEnumerable<Like>> GetAllLikes();
        Task<Like> GetLikeById(Guid id);
        Task<Like> AddLike(Like like);
        Task DeleteLike(Guid id);
    }
}
