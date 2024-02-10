using HouYun3.Models;

namespace HouYun3.IRepositories
{
    public interface IVideoRepository
    {
        Task<IEnumerable<Video>> GetAllVideos();
        Task<IEnumerable<Video>> GetVideosByCategory(string categoryName);
        Task<Video> GetVideoById(Guid id);
        Task AddVideo(Video video, IFormFile videoFile);
        Task<Video> UpdateVideo(Video video);
        Task DeleteVideo(Guid id);
        Task<IEnumerable<Video>> SearchVideosByTitle(string searchTerm);
        Task<IEnumerable<Video>> GetAllVideosByChannelName(string ChannelName);
    }
}
