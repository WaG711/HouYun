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

        public async Task<Video> GetVideoByIdAsync(int id)
        {
            return await _context.Videos.FindAsync(id);
        }

        public async Task<List<Video>> GetAllVideosAsync()
        {
            return await _context.Videos.ToListAsync();
        }

        public async Task<List<Video>> GetVideosByUserIdAsync(string userId)
        {
            return await _context.Videos
                .Where(v => v.UserId == userId)
                .ToListAsync();
        }

        public async Task<List<Video>> GetVideosByCategoryIdAsync(int categoryId)
        {
            return await _context.Videos.Where(v => v.CategoryId == categoryId).ToListAsync();
        }

        public async Task AddVideoAsync(Video video, IFormFile videoFile)
        {
            if (videoFile != null && videoFile.Length > 0)
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(videoFile.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "videos", fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await videoFile.CopyToAsync(stream);
                }

                video.FilePath = fileName;
            }

            _context.Videos.Add(video);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateVideoAsync(Video video)
        {
            _context.Videos.Update(video);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteVideoAsync(int id)
        {
            var video = await _context.Videos.FindAsync(id);
            if (video != null)
            {
                _context.Videos.Remove(video);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Video>> SearchVideosByTitleAsync(string searchTerm)
        {
            return await _context.Videos
                .Where(v => v.Title.Contains(searchTerm))
                .ToListAsync();
        }
    }
}
