using HouYun3.Models;

namespace HouYun3.IRepositories
{
    public interface IWatchHistoryRepository
    {
        Task<IEnumerable<WatchHistory>> GetAllWatchHistory();
        Task<WatchHistory> GetWatchHistoryById(int watchHistoryId);
        Task AddWatchHistory(WatchHistory watchHistory);
        Task DeleteWatchHistory(int watchHistoryId);
    }
}
