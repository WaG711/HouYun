using HouYun.Models;

namespace HouYun.IRepositories
{
    public interface IWatchHistoryRepository
    {
        Task<WatchHistory> GetWatchHistoryByChannelIdAndVideoId(Guid channelId, Guid videoId);
        Task<IEnumerable<Video>> GetWatchHistoryByChannelId(Guid channelId);
        Task AddWatchHistory(WatchHistory watchHistory);
        Task UpdateWatchHistory(WatchHistory existingWatchHistory);
        Task DeleteAllWatchHistory(Guid channelId);
    }
}