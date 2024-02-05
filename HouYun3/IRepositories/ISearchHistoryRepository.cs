using HouYun3.Models;

namespace HouYun3.IRepositories
{
    public interface ISearchHistoryRepository
    {
        Task<IEnumerable<SearchHistory>> GetSearchHistoryByUserId(int userId);
        Task AddSearchHistory(SearchHistory searchHistory);
    }
}
