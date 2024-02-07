using HouYun3.Models;

namespace HouYun3.IRepositories
{
    public interface IWatchLaterRepository
    {
        Task<IEnumerable<WatchLater>> GetAllWatchLaterItems();
        Task<WatchLater> GetWatchLaterItemById(Guid id);
        Task AddWatchLaterItem(WatchLater watchLaterItem);
        Task DeleteWatchLaterItem(Guid id);
    }
}
