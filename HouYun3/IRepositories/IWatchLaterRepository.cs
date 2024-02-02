using HouYun3.Models;

namespace HouYun3.IRepositories
{
    public interface IWatchLaterRepository
    {
        Task<List<WatchLater>> GetWatchLaterList(int userId);
        Task<WatchLater> GetWatchLater(int watchLaterId);
        Task AddWatchLater(WatchLater watchLater);
        Task DeleteWatchLater(int watchLaterId);
    }
}
