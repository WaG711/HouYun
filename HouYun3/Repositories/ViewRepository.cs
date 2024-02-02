using HouYun3.IRepositories;
using HouYun3.Models;
using HouYun3.Data;
using Microsoft.EntityFrameworkCore;

namespace HouYun3.Repositories
{
    public class ViewRepository : IViewRepository
    {
        private readonly HouYun3Context _context;

        public ViewRepository(HouYun3Context context)
        {
            _context = context;
        }

        public async Task<List<View>> GetViews(int videoId)
        {
            return await _context.Views
                .Where(v => v.VideoID == videoId)
                .Include(v => v.User)
                .ToListAsync();
        }

        public async Task AddView(View view)
        {
            _context.Views.Add(view);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteView(int viewId)
        {
            var view = await _context.Views.FindAsync(viewId);
            if (view != null)
            {
                _context.Views.Remove(view);
                await _context.SaveChangesAsync();
            }
        }
    }
}
