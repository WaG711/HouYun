using HouYun3.IRepositories;
using HouYun3.Models;
using HouYun3.Data;
using Microsoft.EntityFrameworkCore;

namespace HouYun3.Repositories
{
    public class WatchLaterRepository : IWatchLaterRepository
    {
        private readonly HouYun3Context _context;

        public WatchLaterRepository(HouYun3Context context)
        {
            _context = context;
        }

        public async Task<IEnumerable<WatchLater>> GetAllWatchLater()
        {
            return await _context.WatchLaters
                .Include(w => w.User)
                .Include(w => w.Video)
                .ToListAsync();
        }

        public async Task<WatchLater> GetWatchLaterById(int watchLaterId)
        {
            return await _context.WatchLaters
                .Include(w => w.User)
                .Include(w => w.Video)
                .FirstOrDefaultAsync(w => w.WatchLaterId == watchLaterId);
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
