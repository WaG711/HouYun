using HouYun3.Models;

namespace HouYun3.IRepositories
{
    public interface ISearchHistoryRepository
    {
        Task<IEnumerable<SearchHistory>> GetAllSearchHistory();
        Task<SearchHistory> GetSearchHistoryById(Guid id);
        Task<SearchHistory> AddSearchHistory(SearchHistory searchHistory);
        Task DeleteSearchHistory(Guid id);
    }
}
