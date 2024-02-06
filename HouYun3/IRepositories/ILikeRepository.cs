using HouYun3.Models;

namespace HouYun3.IRepositories
{
    public interface ILikeRepository
    {
        Task<Like> GetLikeByIdAsync(int id);
        Task<List<Like>> GetAllLikesAsync();
        Task AddLikeAsync(Like like);
        Task DeleteLikeAsync(int id);
    }
}
