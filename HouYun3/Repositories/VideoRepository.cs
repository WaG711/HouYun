using HouYun3.IRepositories;
using HouYun3.Models;
using HouYun3.Data;
using Microsoft.EntityFrameworkCore;

namespace HouYun3.Repositories
{
    public class VideoRepository : IVideoRepository
    {
        private readonly HouYun3Context _context;

        public VideoRepository(HouYun3Context context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Video>> GetAllVideos()
        {
            return await _context.Videos
                .Include(v => v.Likes)
                .Include(v => v.Views)
                .Include(v => v.Category)
                .Include(v => v.User)
                .Include(v => v.Comments)
                .ToListAsync();
        }

        public async Task<IEnumerable<Video>> GetUserVideos(int userId)
        {
            return await _context.Videos
                .Include(v => v.Likes)
                .Include(v => v.Views)
                .Include(v => v.Category)
                .Include(v => v.User)
                .Include(v => v.Comments)
                .Where(v => v.User.Id == userId.ToString())
                .ToListAsync();
        }

        public async Task<Video> GetVideoById(int videoId)
        {
            return await _context.Videos
                .Include(v => v.Likes)
                .Include(v => v.Views)
                .Include(v => v.Category)
                .Include(v => v.User)
                .Include(v => v.Comments)
                .FirstOrDefaultAsync(v => v.VideoId == videoId);
        }

        public async Task AddVideo(Video video)
        {
            _context.Videos.Add(video);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateVideo(Video video)
        {
            _context.Entry(video).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteVideo(int videoId)
        {
            var video = await _context.Videos.FindAsync(videoId);
            if (video != null)
            {
                _context.Videos.Remove(video);
                await _context.SaveChangesAsync();
            }
        }


        public async Task<IEnumerable<Video>> SearchVideosByTitle(string searchTerm)
        {
            return await _context.Videos
                .Where(v => v.Title.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
                .ToListAsync();
        }
    }
}
