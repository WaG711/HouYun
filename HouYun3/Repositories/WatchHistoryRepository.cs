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

        public async Task<IEnumerable<WatchHistory>> GetWatchHistoryByUserId(int userId)
        {
            return await _context.WatchHistories
                .Where(w => w.UserId == userId)
                .Include(w => w.User)
                .Include(w => w.Video)
                .ToListAsync();
        }

        public async Task<WatchHistory> GetWatchHistoryById(int watchHistoryId)
        {
            return await _context.WatchHistories
                .Include(w => w.User)
                .Include(w => w.Video)
                .FirstOrDefaultAsync(w => w.WatchHistoryId == watchHistoryId);
        }

        public async Task AddWatchHistory(WatchHistory watchHistory)
        {
            _context.WatchHistories.Add(watchHistory);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteWatchHistory(int watchHistoryId)
        {
            var watchHistory = await _context.WatchHistories.FindAsync(watchHistoryId);
            if (watchHistory != null)
            {
                _context.WatchHistories.Remove(watchHistory);
                await _context.SaveChangesAsync();
            }
        }
    }
}
