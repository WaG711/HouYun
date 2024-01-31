using HouYun2.Models;

namespace HouYun2.IRepositories
{
    public interface IVideoRepository
    {
        Task<List<Video>> GetAllVideos();
        Task<Video> GetVideo(int videoId);
        Task<List<Video>> GetVideosByCategory(int categoryId);
        Task<List<Video>> GetVideosByUser(int userId);
        Task AddVideo(Video video);
        Task UpdateVideo(Video video);
        Task DeleteVideo(int videoId);

    }
}
