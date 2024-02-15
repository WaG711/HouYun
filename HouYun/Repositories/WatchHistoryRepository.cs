using HouYun.IRepositories;
using HouYun.Models;
using HouYun.Data;
using Microsoft.EntityFrameworkCore;

namespace HouYun.Repositories
{
    public class WatchHistoryRepository : IWatchHistoryRepository
    {
        private readonly ApplicationDbContext _context;

        public WatchHistoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<WatchHistory> GetWatchHistoryByChannelIdAndVideoId(Guid channelId, Guid videoId)
        {
            return await _context.WatchHistories
                .FirstOrDefaultAsync(w => w.ChannelId == channelId && w.VideoId == videoId);
        }

        public async Task UpdateWatchHistory(WatchHistory existingWatchHistory)
        {
            _context.Entry(existingWatchHistory).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<WatchHistory>> GetWatchHistoryByChannelId(Guid channelId)
        {
            return await _context.WatchHistories
            .Where(w => w.ChannelId == channelId)
            .Include(w => w.Channel)
            .Include(w => w.Video)
                .ThenInclude(v => v.Channel)
            .Include(w => w.Video)
                .ThenInclude(v => v.Views)
            .Include(w => w.Video)
                .ThenInclude(v => v.Comments)
            .OrderByDescending(w => w.WatchDate)
            .ToListAsync();
        }

        public async Task AddWatchHistory(WatchHistory watchHistory)
        {
            _context.WatchHistories.Add(watchHistory);
            await _context.SaveChangesAsync();
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