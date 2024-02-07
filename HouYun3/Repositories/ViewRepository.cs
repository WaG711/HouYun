using HouYun3.IRepositories;
using HouYun3.Models;
using HouYun3.Data;
using Microsoft.EntityFrameworkCore;

namespace HouYun3.Repositories
{
    public class ViewRepository : IViewRepository
    {
        private readonly ApplicationDbContext _context;

        public ViewRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<View>> GetAllViews()
        {
            return await _context.Views.ToListAsync();
        }

        public async Task<View> GetViewById(Guid id)
        {
            return await _context.Views.FindAsync(id);
        }

        public async Task AddView(View view)
        {
            _context.Views.Add(view);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteView(Guid id)
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
