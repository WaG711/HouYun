using HouYun3.Models;

namespace HouYun3.IRepositories
{
    public interface IWatchLaterRepository
    {
        Task<IEnumerable<Video>> GetVideosByUserId(string userId);
        Task<WatchLater> GetWatchLaterItemById(Guid id);
        Task AddWatchLaterItem(WatchLater watchLaterItem);
        Task DeleteWatchLaterItem(Guid id);
        Task<WatchLater> GetWatchLaterItemByUserIdAndVideoId(string userId, Guid videoId);
    }
}
