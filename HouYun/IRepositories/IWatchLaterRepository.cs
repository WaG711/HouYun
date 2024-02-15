using HouYun.Models;

namespace HouYun.IRepositories
{
    public interface IWatchLaterRepository
    {
        Task<IEnumerable<Video>> GetVideosByChannelId(Guid channelId);
        Task AddWatchLaterItem(WatchLater watchLaterItem);
        Task DeleteWatchLaterItem(Guid id);
        Task<WatchLater> GetWatchLaterItemByChannelIdAndVideoId(Guid channelId, Guid videoId);
    }
}
