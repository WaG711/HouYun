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

        public async Task<IEnumerable<WatchHistory>> GetVideosByChannelId(Guid channelId)
        {
            var watchHistoryItems = await _context.WatchHistories
                .Where(w => w.ChannelId == channelId)
                .OrderByDescending(w => w.WatchDate)
                .ToListAsync();

            return watchHistoryItems;
        }


        public async Task<IEnumerable<WatchHistory>> GetWatchHistoryByChannelId(Guid channelId)
        {
            var watchHistoryItems = await _context.WatchHistories
                .Where(w => w.ChannelId == channelId)
                .OrderByDescending(w => w.WatchDate)
                .ToListAsync();

            return watchHistoryItems;
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

        public async Task<bool> CheckWatchHistoryExists(Guid channelId, Guid videoId)
        {
            var watchHistoryExists = await _context.WatchHistories
                .AnyAsync(w => w.ChannelId == channelId && w.VideoId == videoId);

            return watchHistoryExists;
        }

        public async Task<IEnumerable<WatchHistory>> GetWatchHistoryByUserId(string userId)
        {
            return await _context.WatchHistories
                .Include(w => w.Video)
                .Include(w => w.Channel)
                .Where(w => w.Channel.UserId == userId)
                .ToListAsync();
        }
        public async Task<WatchHistory> GetWatchHistoryByChannelAndVideoId(Guid channelId, Guid videoId)
        {
            return await _context.WatchHistories
                .FirstOrDefaultAsync(w => w.ChannelId == channelId && w.VideoId == videoId);
        }

        public async Task UpdateWatchHistory(WatchHistory watchHistory)
        {
            _context.WatchHistories.Update(watchHistory);
            await _context.SaveChangesAsync();
        }

    }
}
