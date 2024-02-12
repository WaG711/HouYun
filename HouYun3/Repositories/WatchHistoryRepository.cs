using HouYun3.IRepositories;
using HouYun3.Models;
using HouYun3.Data;
using Microsoft.EntityFrameworkCore;

namespace HouYun3.Repositories
{
    public class WatchHistoryRepository : IWatchHistoryRepository
    {
        private readonly ApplicationDbContext _context;

        public WatchHistoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<WatchHistory>> GetAllWatchHistory()
        {
            return await _context.WatchHistories.ToListAsync();
        }

        public async Task<WatchHistory> GetWatchHistoryById(Guid id)
        {
            return await _context.WatchHistories.FindAsync(id);
        }

        public async Task AddWatchHistory(WatchHistory watchHistory)
        {
            _context.WatchHistories.Add(watchHistory);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteWatchHistory(Guid id)
        {
            var watchHistory = await _context.WatchHistories.FindAsync(id);
            if (watchHistory != null)
            {
                _context.WatchHistories.Remove(watchHistory);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteAllWatchHistory(Guid channelId)
        {
            var watchHistories = await _context.WatchHistories
                .Where(w => w.ChannelId == channelId)
                .ToListAsync();

            _context.WatchHistories.RemoveRange(watchHistories);
            await _context.SaveChangesAsync();
        }
    }
}