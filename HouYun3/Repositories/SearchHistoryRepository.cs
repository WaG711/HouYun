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

        public async Task<IEnumerable<SearchHistory>> GetSearchHistoryByUserId(string userId)
        {
            return await _context.SearchHistories
                .Where(sh => sh.User.Id == userId)
                .OrderByDescending(sh => sh.SearchDate)
                .Take(10)
                .ToListAsync();
        }


        public async Task AddSearchHistory(SearchHistory searchHistory)
        {
            _context.SearchHistories.Add(searchHistory);
            await _context.SaveChangesAsync();
        }

        public Task<IEnumerable<SearchHistory>> GetSearchHistoryByUserId(int userId)
        {
            throw new NotImplementedException();
        }
    }
}
