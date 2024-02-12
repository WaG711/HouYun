using HouYun3.Models;

namespace HouYun3.IRepositories
{
    public interface ISearchHistoryRepository
    {
        Task<SearchHistory> AddSearchHistory(SearchHistory searchHistory);
        Task<IEnumerable<SearchHistory>> GetSearchHistoryByChannelId(Guid channelId);
        Task<IEnumerable<Video>> SearchVideosByTitle(string searchTerm);
        Task<SearchHistory> GetSearchHistoryByChannelIdAndQuery(Guid channelId, string searchTerm);
    }
}
