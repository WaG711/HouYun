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

        public async Task<IEnumerable<SearchHistory>> GetAllSearchHistory()
        {
            return await _context.SearchHistories.ToListAsync();
        }

        public async Task<SearchHistory> GetSearchHistoryById(Guid id)
        {
            return await _context.SearchHistories.FindAsync(id);
        }

        public async Task<SearchHistory> AddSearchHistory(SearchHistory searchHistory)
        {
            _context.SearchHistories.Add(searchHistory);
            await _context.SaveChangesAsync();
            return searchHistory;
        }

        public async Task DeleteSearchHistory(Guid id)
        {
            var searchHistory = await _context.SearchHistories.FindAsync(id);
            if (searchHistory != null)
            {
                _context.SearchHistories.Remove(searchHistory);
                await _context.SaveChangesAsync();
            }
        }
    }
}
