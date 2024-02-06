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

        public async Task<WatchLater> GetWatchLaterByIdAsync(int id)
        {
            return await _context.WatchLaters.FindAsync(id);
        }

        public async Task<List<WatchLater>> GetAllWatchLatersAsync()
        {
            return await _context.WatchLaters.ToListAsync();
        }

        public async Task AddWatchLaterAsync(WatchLater watchLater)
        {
            _context.WatchLaters.Add(watchLater);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteWatchLaterAsync(int id)
        {
            var watchLater = await _context.WatchLaters.FindAsync(id);
            if (watchLater != null)
            {
                _context.WatchLaters.Remove(watchLater);
                await _context.SaveChangesAsync();
            }
        }
    }
}
