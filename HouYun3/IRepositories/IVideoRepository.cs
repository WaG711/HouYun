using HouYun3.Models;

namespace HouYun3.IRepositories
{
    public interface IVideoRepository
    {
        Task<Video> GetVideoByIdAsync(int id);
        Task<List<Video>> GetAllVideosAsync();
        Task<List<Video>> GetVideosByCategoryIdAsync(int categoryId);
        Task<List<Video>> GetVideosByUserIdAsync(string userId);
        Task AddVideoAsync(Video video, IFormFile videoFile);
        Task UpdateVideoAsync(Video video);
        Task DeleteVideoAsync(int id);
        Task<List<Video>> SearchVideosByTitleAsync(string searchTerm);
    }
}
