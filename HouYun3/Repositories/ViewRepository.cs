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

        public async Task<View> GetViewByVideoAndChannel(Guid videoId, Guid channelId)
        {
            return await _context.Views
                .FirstOrDefaultAsync(v => v.VideoId == videoId && v.ChannelId == channelId);
        }

        public async Task AddView(View view)
        {
            _context.Views.Add(view);
            await _context.SaveChangesAsync();
        }
    }
}
