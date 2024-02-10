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

        public async Task<IEnumerable<Video>> GetVideosByUserId(string userId)
        {
            var watchLaterItems = await _context.WatchLaterItems
                .Include(v => v.User)
                .Include(v => v.Video)
                    .ThenInclude(v => v.Views)
                .Where(v => v.UserId == userId)
                .ToListAsync();

            var videoIds = watchLaterItems.Select(v => v.VideoId).ToList();

            var videos = await _context.Videos
                .Where(v => videoIds.Contains(v.VideoId))
                .ToListAsync();

            return videos;
        }

        public async Task<WatchLater> GetWatchLaterItemById(Guid id)
        {
            return await _context.WatchLaterItems.FindAsync(id);
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
    }
}
