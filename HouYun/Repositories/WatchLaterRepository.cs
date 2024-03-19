using HouYun.IRepositories;
using HouYun.Models;
using HouYun.Data;
using Microsoft.EntityFrameworkCore;

namespace HouYun.Repositories
{
    public class WatchLaterRepository : IWatchLaterRepository
    {
        private readonly ApplicationDbContext _context;

        public WatchLaterRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Video>> GetVideosByChannelId(Guid channelId)
        {
            return await _context.WatchLaterItems
                .Where(wl => wl.ChannelId == channelId)
                .Include(wl => wl.Video.Channel)
                .Include(wl => wl.Video.Views)
                .Include(wl => wl.Video.WatchLaterItems)
                    .ThenInclude(wl => wl.Channel)
                .Select(wl => wl.Video)
                .ToListAsync();
        }

        public async Task AddWatchLaterItem(WatchLater watchLaterItem)
        {
            _context.WatchLaterItems.Add(watchLaterItem);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteWatchLaterItem(Guid id)
        {
            var watchLaterItem = await _context.WatchLaterItems.FindAsync(id);
            if (watchLaterItem != null)
            {
                _context.WatchLaterItems.Remove(watchLaterItem);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<WatchLater> GetWatchLaterItemByChannelIdAndVideoId(Guid channelId, Guid videoId)
        {
            return await _context.WatchLaterItems
                .FirstOrDefaultAsync(wl => wl.ChannelId == channelId && wl.VideoId == videoId);
        }
    }
}
