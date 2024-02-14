using HouYun3.Models;

namespace HouYun3.IRepositories
{
    public interface IVideoRepository
    {
        Task<IEnumerable<Video>> GetAllVideos();
        Task<IEnumerable<Video>> GetVideosByCategory(string categoryName);
        Task<Video> GetVideoById(Guid id);
    }
}
