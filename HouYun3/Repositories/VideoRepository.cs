using HouYun3.IRepositories;
using HouYun3.Models;
using HouYun3.Data;
using Microsoft.EntityFrameworkCore;

namespace HouYun3.Repositories
{
    public class VideoRepository : IVideoRepository
    {
        private readonly ApplicationDbContext _context;

        public VideoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Video>> GetAllVideos()
        {
            return await _context.Videos
                .Include(v => v.Category)
                .Include(v => v.Channel)
                .Include(v => v.Views)
                .OrderByDescending(v => v.UploadDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<Video>> GetVideosByCategory(string categoryName)
        {
            return await _context.Videos
                .Include(v => v.Category)
                .Include(v => v.Channel)
                .Include(v => v.Views)
                .Where(v => v.Category.Name == categoryName)
                .ToListAsync();
        }

        public async Task<Video> GetVideoById(Guid id)
        {
            return await _context.Videos
                .Include(v => v.Category)
                .Include(v => v.Channel)
                .Include(v => v.Comments)
                    .ThenInclude(v => v.Channel)
                .Include(v => v.Likes)
                .Include(v => v.Views)
                .FirstOrDefaultAsync(v => v.VideoId == id);
        }
    }
}
