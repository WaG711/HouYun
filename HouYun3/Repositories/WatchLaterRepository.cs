using HouYun3.IRepositories;
using HouYun3.Models;
using HouYun3.Data;
using Microsoft.EntityFrameworkCore;

namespace HouYun3.Repositories
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
            var videos = await _context.WatchLaterItems
                .Where(item => item.ChannelId == channelId)
                .Include(item => item.Video.Channel)
                .Include(item => item.Video.Views)
                .Include(item => item.Video.WatchLaterItems)
                    .ThenInclude(v => v.Channel)
                .Select(item => item.Video)
                .ToListAsync();

            return videos;
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
