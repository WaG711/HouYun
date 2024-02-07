using HouYun3.Models;

namespace HouYun3.IRepositories
{
    public interface IWatchHistoryRepository
    {
        Task<IEnumerable<WatchHistory>> GetAllWatchHistory();
        Task<WatchHistory> GetWatchHistoryById(Guid id);
        Task AddWatchHistory(WatchHistory watchHistory);
        Task DeleteWatchHistory(Guid id);
    }
}
