using HouYun3.Models;

namespace HouYun3.IRepositories
{
    public interface IWatchHistoryRepository
    {
        Task<WatchHistory> GetWatchHistoryByIdAsync(int id);
        Task<List<WatchHistory>> GetAllWatchHistoriesAsync();
        Task AddWatchHistoryAsync(WatchHistory watchHistory);
        Task DeleteWatchHistoryAsync(int id);
    }
}
