using HouYun3.Models;

namespace HouYun3.IRepositories
{
    public interface IWatchLaterRepository
    {
        Task<IEnumerable<WatchLater>> GetWatchLaterByUserId(int userId);
        Task<WatchLater> GetWatchLaterById(int watchLaterId);
        Task AddWatchLater(WatchLater watchLater);
        Task DeleteWatchLater(int watchLaterId);
    }
}
