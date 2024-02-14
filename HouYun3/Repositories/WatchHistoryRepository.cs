﻿using HouYun3.IRepositories;
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