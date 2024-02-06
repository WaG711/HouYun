using HouYun3.Models;

namespace HouYun3.IRepositories
{
    public interface IWatchLaterRepository
    {
        Task<WatchLater> GetWatchLaterByIdAsync(int id);
        Task<List<WatchLater>> GetAllWatchLatersAsync();
        Task AddWatchLaterAsync(WatchLater watchLater);
        Task DeleteWatchLaterAsync(int id);
    }
}
