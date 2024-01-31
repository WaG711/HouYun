using HouYun2.IRepositories;
using HouYun2.Models;
using HouYun3.Data;
using Microsoft.EntityFrameworkCore;

namespace HouYun2.Repositories
{
    public class WatchLaterRepository : IWatchLaterRepository
    {
        private readonly HouYun3Context _context;

        public WatchLaterRepository(HouYun3Context context)
        {
            _context = context;
        }

        public async Task<List<WatchLater>> GetWatchLaterList(int userId)
        {
            return await _context.WatchLaters
                .Where(wl => wl.UserID == userId)
                .Include(wl => wl.Video)
                .ToListAsync();
        }

        public async Task<WatchLater> GetWatchLater(int watchLaterId)
        {
            return await _context.WatchLaters
                .Include(wl => wl.User)
                .Include(wl => wl.Video)
                .FirstOrDefaultAsync(wl => wl.WatchLaterID == watchLaterId);
        }

        public async Task AddWatchLater(WatchLater watchLater)
        {
            _context.WatchLaters.Add(watchLater);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteWatchLater(int watchLaterId)
        {
            var watchLater = await _context.WatchLaters.FindAsync(watchLaterId);
            if (watchLater != null)
            {
                _context.WatchLaters.Remove(watchLater);
                await _context.SaveChangesAsync();
            }
        }
    }
}
