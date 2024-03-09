using HouYun.Models;

namespace HouYun.IRepositories
{
    public interface ISearchHistoryRepository
    {
        Task<IEnumerable<Video>> SearchVideosByTitle(string searchTerm);
    }
}
