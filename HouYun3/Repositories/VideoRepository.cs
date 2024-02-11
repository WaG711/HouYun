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
                .Include(v => v.User)
                .Include(v => v.Views)
                .ToListAsync();
        }

        public async Task<IEnumerable<Video>> GetVideosByCategory(string categoryName)
        {
            return await _context.Videos
                .Include(v => v.Category)
                .Include(v => v.User)
                .Include(v => v.Views)
                .Where(v => v.Category.Name == categoryName)
                .ToListAsync();
        }

        public async Task<Video> GetVideoById(Guid id)
        {
            return await _context.Videos
                .Include(v => v.Category)
                .Include(v => v.User)
                .Include(v => v.Comments)
                    .ThenInclude(v => v.User)
                .Include(v => v.Likes)
                .Include(v => v.Views)
                .FirstOrDefaultAsync(v => v.VideoId == id);
        }

        public async Task AddVideo(Video video, IFormFile videoFile)
        {
            try
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(videoFile.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "videos", fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await videoFile.CopyToAsync(stream);
                }

                video.FilePath = fileName;

                _context.Videos.Add(video);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                if (!string.IsNullOrEmpty(video.FilePath))
                {
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "videos", video.FilePath);

                    if (File.Exists(filePath))
                    {
                        File.Delete(filePath);
                    }
                }
                throw;
            }
        }

        public async Task<Video> UpdateVideo(Video video)
        {
            _context.Entry(video).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return video;
        }

        public async Task DeleteVideo(Guid id)
        {
            var video = await _context.Videos.FindAsync(id);
            if (video != null)
            {
                _context.Videos.Remove(video);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Video>> SearchVideosByTitle(string searchTerm)
        {
            return await _context.Videos
                .Include(v => v.Category)
                .Include(v => v.User)
                .Include(v => v.Views)
                .Where(v => v.Title.Contains(searchTerm))
                .ToListAsync();
        }

        public async Task<IEnumerable<Video>> GetAllVideosByChannelName(string ChannelName)
        {
            return await _context.Videos
                .Include(v => v.Category)
                .Include(v => v.User)
                .Include(v => v.Views)
                .Where(v => v.User.UserName == ChannelName)
                .ToListAsync();
        }
    }
}
