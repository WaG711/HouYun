using HouYun3.IRepositories;
using HouYun3.Models;
using HouYun3.Data;
using Microsoft.EntityFrameworkCore;

namespace HouYun3.Repositories
{
    public class SearchHistoryRepository : ISearchHistoryRepository
    {
        private readonly HouYun3Context _context;

        public SearchHistoryRepository(HouYun3Context context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SearchHistory>> GetSearchHistoryByUserId(int userId)
        {
            return await _context.SearchHistories
                .Where(sh => sh.User.UserId == userId)
                .OrderByDescending(sh => sh.SearchDate)
                .ToListAsync();
        }

        public async Task AddSearchHistory(SearchHistory searchHistory)
        {
            _context.SearchHistories.Add(searchHistory);
            await _context.SaveChangesAsync();
        }
    }
}
