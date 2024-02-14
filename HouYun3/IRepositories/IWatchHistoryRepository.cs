using HouYun3.Models;

namespace HouYun3.IRepositories
{
    public interface IWatchHistoryRepository
    {
        Task<WatchHistory> GetWatchHistoryByChannelIdAndVideoId(Guid channelId, Guid videoId);
        Task<IEnumerable<WatchHistory>> GetWatchHistoryByChannelId(Guid channelId);
        Task AddWatchHistory(WatchHistory watchHistory);
        Task DeleteWatchHistory(Guid id);
        Task UpdateWatchHistory(WatchHistory existingWatchHistory);
        Task DeleteAllWatchHistory(Guid channelId);
    }
}