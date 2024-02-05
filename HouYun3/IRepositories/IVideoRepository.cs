using HouYun3.Models;

namespace HouYun3.IRepositories
{
    public interface IVideoRepository
    {
        Task<IEnumerable<Video>> GetAllVideos();
        Task<IEnumerable<Video>> GetUserVideos(int userId);
        Task<Video> GetVideoById(int videoId);
        Task AddVideo(Video video);
        Task UpdateVideo(Video video);
        Task DeleteVideo(int videoId);
        Task<IEnumerable<Video>> SearchVideosByTitle(string searchTerm);
    }
}
