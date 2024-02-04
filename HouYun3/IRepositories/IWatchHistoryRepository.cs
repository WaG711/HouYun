using HouYun3.Models;

namespace HouYun3.IRepositories
{
    public interface IWatchHistoryRepository
    {
        Task<IEnumerable<WatchHistory>> GetWatchHistoryByUserId(int userId);
        Task<WatchHistory> GetWatchHistoryById(int watchHistoryId);
        Task AddWatchHistory(WatchHistory watchHistory);
        Task DeleteWatchHistory(int watchHistoryId);
    }
}
