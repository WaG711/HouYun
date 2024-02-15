using HouYun.IRepositories;
using HouYun.Models;
using HouYun.Data;
using Microsoft.EntityFrameworkCore;

namespace HouYun.Repositories
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

        public async Task<SearchHistory> GetSearchHistoryByChannelIdAndQuery(Guid channelId, string searchTerm)
        {
            return await _context.SearchHistories
                .FirstOrDefaultAsync(s => s.ChannelId == channelId && s.SearchQuery == searchTerm);
        }

        public async Task<IEnumerable<Video>> SearchVideosByTitle(string searchTerm)
        {
            return await _context.Videos
                .Include(v => v.Category)
                .Include(v => v.Channel)
                .Include(v => v.Views)
                .Where(v => v.Title.Contains(searchTerm))
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
