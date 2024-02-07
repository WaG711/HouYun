using HouYun3.Models;

namespace HouYun3.IRepositories
{
    public interface ISearchHistoryRepository
    {
        Task<SearchHistory> AddSearchHistory(SearchHistory searchHistory);
        Task<IEnumerable<SearchHistory>> GetSearchHistoryByUserId(string userId);
    }
}
