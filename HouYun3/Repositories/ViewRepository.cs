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

        public async Task<View> GetViewByIdAsync(int id)
        {
            return await _context.Views.FindAsync(id);
        }

        public async Task<List<View>> GetAllViewsAsync()
        {
            return await _context.Views.ToListAsync();
        }

        public async Task AddViewAsync(View view)
        {
            _context.Views.Add(view);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteViewAsync(int id)
        {
            var view = await _context.Views.FindAsync(id);
            if (view != null)
            {
                _context.Views.Remove(view);
                await _context.SaveChangesAsync();
            }
        }
    }
}
