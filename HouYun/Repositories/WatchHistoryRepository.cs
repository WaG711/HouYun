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
                .FirstOrDefaultAsync(wh => wh.ChannelId == channelId && wh.VideoId == videoId);
        }

        public async Task UpdateWatchHistory(WatchHistory existingWatchHistory)
        {
            _context.Entry(existingWatchHistory).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Video>> GetWatchHistoryByChannelId(Guid channelId)
        {
            return await _context.WatchHistories
            .Where(wh => wh.ChannelId == channelId)
            .Include(wh => wh.Channel)
            .Include(wh => wh.Video)
                .ThenInclude(v => v.Channel)
            .Include(wh => wh.Video)
                .ThenInclude(v => v.Views)
            .Include(wh => wh.Video)
                .ThenInclude(v => v.Comments)
            .OrderByDescending(wh => wh.WatchDate)
            .Select(wh => wh.Video)
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
                .Where(wh => wh.ChannelId == channelId)
                .ToListAsync();

            _context.WatchHistories.RemoveRange(watchHistories);
            await _context.SaveChangesAsync();
        }
    }
}