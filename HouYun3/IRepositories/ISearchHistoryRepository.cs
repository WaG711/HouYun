using HouYun3.Models;

namespace HouYun3.IRepositories
{
    public interface ISearchHistoryRepository
    {
        Task<SearchHistory> GetSearchHistoryByIdAsync(int id);
        Task<List<SearchHistory>> GetAllSearchHistoriesAsync();
        Task AddSearchHistoryAsync(SearchHistory searchHistory);
        Task UpdateSearchHistoryAsync(SearchHistory searchHistory);
        Task DeleteSearchHistoryAsync(int id);
    }
}
