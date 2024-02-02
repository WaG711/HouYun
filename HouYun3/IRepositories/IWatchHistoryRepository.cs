using HouYun3.Models;

namespace HouYun3.IRepositories
{
    public interface IWatchHistoryRepository
    {
        Task<List<WatchHistory>> GetWatchHistories(int userId);
        Task<WatchHistory> GetWatchHistory(int watchHistoryId);
        Task AddWatchHistory(WatchHistory watchHistory);
        Task DeleteWatchHistory(int watchHistoryId);

    }
}
