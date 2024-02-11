using HouYun3.Models;

namespace HouYun3.IRepositories
{
    public interface IWatchLaterRepository
    {
        Task<IEnumerable<Video>> GetVideosByChannelId(Guid channelId);
        Task<WatchLater> GetWatchLaterItemById(Guid id);
        Task AddWatchLaterItem(WatchLater watchLaterItem);
        Task DeleteWatchLaterItem(Guid id);
        Task<WatchLater> GetWatchLaterItemByChannelIdAndVideoId(Guid channelId, Guid videoId);
    }
}
