using HouYun3.Models;

namespace HouYun3.IRepositories
{
    public interface ISearchHistoryRepository
    {
        Task<List<SearchHistory>> GetSearchHistories(int userId);
        Task<SearchHistory> GetSearchHistory(int searchHistoryId);
        Task AddSearchHistory(SearchHistory searchHistory);
        Task DeleteSearchHistory(int searchHistoryId);
    }
}
