using HouYun3.IRepositories;
using HouYun3.Models;
using HouYun3.Data;
using Microsoft.EntityFrameworkCore;

namespace HouYun3.Repositories
{
    public class SearchHistoryRepository : ISearchHistoryRepository
    {
        private readonly ApplicationDbContext _context;

        public SearchHistoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SearchHistory>> GetSearchHistoryByChannelId(Guid channelId)
        {
            return await _context.SearchHistories
                .Where(s => s.ChannelId == channelId)
                .OrderByDescending(s => s.SearchDate)
                .Take(10)
                .ToListAsync();
        }

        public async Task<SearchHistory> AddSearchHistory(SearchHistory searchHistory)
        {
            _context.SearchHistories.Add(searchHistory);
            await _context.SaveChangesAsync();
            return searchHistory;
        }
    }
}
