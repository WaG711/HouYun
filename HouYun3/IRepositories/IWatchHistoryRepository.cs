using HouYun3.Models;

namespace HouYun3.IRepositories
{
    public interface IWatchHistoryRepository
    {
        Task<IEnumerable<WatchHistory>> GetAllWatchHistory();
        Task AddWatchHistory(WatchHistory watchHistory);
        Task DeleteWatchHistory(Guid id);
        Task DeleteAllWatchHistory(Guid channelId);
        Task<IEnumerable<WatchHistory>> GetVideosByChannelId(Guid channelId);
        Task<bool> CheckWatchHistoryExists(Guid userId, Guid videoId);
        Task<IEnumerable<WatchHistory>> GetWatchHistoryByUserId(string userId);
        Task UpdateWatchHistory(WatchHistory watchHistory);
        Task<WatchHistory> GetWatchHistoryByChannelAndVideoId(Guid channelId, Guid videoId);
        Task<IEnumerable<WatchHistory>> GetWatchHistoryByChannelId(Guid channelId);
    }
}
