using HouYun3.IRepositories;
using HouYun3.Models;
using HouYun3.Data;
using Microsoft.EntityFrameworkCore;

namespace HouYun3.Repositories
{
    public class WatchHistoryRepository : IWatchHistoryRepository
    {
        private readonly HouYun3Context _context;

        public WatchHistoryRepository(HouYun3Context context)
        {
            _context = context;
        }

        public async Task<WatchHistory> GetWatchHistoryByIdAsync(int id)
        {
            return await _context.WatchHistories.FindAsync(id);
        }

        public async Task<List<WatchHistory>> GetAllWatchHistoriesAsync()
        {
            return await _context.WatchHistories.ToListAsync();
        }

        public async Task AddWatchHistoryAsync(WatchHistory watchHistory)
        {
            _context.WatchHistories.Add(watchHistory);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteWatchHistoryAsync(int id)
        {
            var watchHistory = await _context.WatchHistories.FindAsync(id);
            if (watchHistory != null)
            {
                _context.WatchHistories.Remove(watchHistory);
                await _context.SaveChangesAsync();
            }
        }
    }
}
