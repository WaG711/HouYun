using HouYun2.IRepositories;
using HouYun2.Models;
using HouYun3.Data;
using Microsoft.EntityFrameworkCore;

namespace HouYun2.Repositories
{
    public class SearchHistoryRepository : ISearchHistoryRepository
    {
        private readonly HouYun3Context _context;

        public SearchHistoryRepository(HouYun3Context context)
        {
            _context = context;
        }

        public async Task<List<SearchHistory>> GetSearchHistories(int userId)
        {
            return await _context.SearchHistories
                .Where(s => s.UserID == userId)
                .ToListAsync();
        }

        public async Task<SearchHistory> GetSearchHistory(int searchHistoryId)
        {
            return await _context.SearchHistories
                .FirstOrDefaultAsync(s => s.SearchHistoryID == searchHistoryId);
        }

        public async Task AddSearchHistory(SearchHistory searchHistory)
        {
            _context.SearchHistories.Add(searchHistory);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteSearchHistory(int searchHistoryId)
        {
            var searchHistory = await _context.SearchHistories.FindAsync(searchHistoryId);
            if (searchHistory != null)
            {
                _context.SearchHistories.Remove(searchHistory);
                await _context.SaveChangesAsync();
            }
        }
    }
}
