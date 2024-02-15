using HouYun.Models;

namespace HouYun.IRepositories
{
    public interface IVideoRepository
    {
        Task<IEnumerable<Video>> GetAllVideos();
        Task<IEnumerable<Video>> GetVideosByCategory(string categoryName);
        Task<IEnumerable<Video>> GetAllVideosExceptId(Guid id);
        Task<Video> GetVideoById(Guid id);
        Task AddVideo(Video video, IFormFile videoFile, IFormFile posterFile);
        Task DeleteVideo(Guid id);
        Task<IEnumerable<Video>> GetVideosByChannelId(Guid channelId);
    }
}
