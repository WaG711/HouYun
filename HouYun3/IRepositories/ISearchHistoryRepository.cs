using HouYun3.Models;

namespace HouYun3.IRepositories
{
    public interface ISearchHistoryRepository
    {
        Task<IEnumerable<SearchHistory>> GetSearchHistoryByUserId(string userId);
        Task AddSearchHistory(SearchHistory searchHistory);
    }
}
