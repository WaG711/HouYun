using HouYun3.Models;

namespace HouYun3.IRepositories
{
    public interface ISearchHistoryRepository
    {
        Task<List<SearchHistory>> GetLastSearchesByUserIdAsync(string userId);
        Task AddSearchHistoryAsync(SearchHistory searchHistory);
    }
}
