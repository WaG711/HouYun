using HouYun3.IRepositories;
using HouYun3.Models;
using HouYun3.Data;
using Microsoft.EntityFrameworkCore;

namespace HouYun3.Repositories
{
    public class VideoRepository : IVideoRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly INotificationRepository _notificationRepository;

        public VideoRepository(ApplicationDbContext context, INotificationRepository notificationRepository)
        {
            _context = context;
            _notificationRepository = notificationRepository;
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

        public async Task<IEnumerable<Video>> GetAllVideosExceptId(Guid id)
        {
            var allVideos = await _context.Videos
                .Include(v => v.Category)
                .Include(v => v.Channel)
                .Include(v => v.Views)
                .OrderByDescending(v => v.UploadDate)
                .ToListAsync();

            var videosExceptId = allVideos.Where(v => v.VideoId != id).ToList();

            return videosExceptId;
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

        public async Task AddVideo(Video video, IFormFile videoFile, IFormFile posterFile)
        {
            try
            {
                var videoFileName = Guid.NewGuid().ToString() + Path.GetExtension(videoFile.FileName);
                var videoFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "videos", videoFileName);

                using (var videoStream = new FileStream(videoFilePath, FileMode.Create))
                {
                    await videoFile.CopyToAsync(videoStream);
                }

                var posterFileName = Guid.NewGuid().ToString() + Path.GetExtension(posterFile.FileName);
                var posterFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "posters", posterFileName);

                using (var posterStream = new FileStream(posterFilePath, FileMode.Create))
                {
                    await posterFile.CopyToAsync(posterStream);
                }

                video.VideoPath = videoFileName;
                video.PosterPath = posterFileName;

                _context.Videos.Add(video);
                await _context.SaveChangesAsync();

                var notification = new Notification()
                {
                    Message = $"На канале {video.Channel.Name}. Вышло новое видео: {video.Title}",
                    ChannelId = video.ChannelId
                };
                await _notificationRepository.AddNotification(notification);
            }
            catch (Exception)
            {
                if (!string.IsNullOrEmpty(video.VideoPath))
                {
                    var videoFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "videos", video.VideoPath);

                    if (File.Exists(videoFilePath))
                    {
                        File.Delete(videoFilePath);
                    }
                }

                if (!string.IsNullOrEmpty(video.PosterPath))
                {
                    var posterFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "posters", video.PosterPath);

                    if (File.Exists(posterFilePath))
                    {
                        File.Delete(posterFilePath);
                    }
                }
                throw;
            }
        }

        public async Task DeleteVideo(Guid id)
        {
            var video = await _context.Videos.FindAsync(id);

            if (video != null)
            {
                try
                {
                    var comments = _context.Comments.Where(c => c.VideoId == id);
                    _context.Comments.RemoveRange(comments);

                    var likes = _context.Likes.Where(l => l.VideoId == id);
                    _context.Likes.RemoveRange(likes);

                    var views = _context.Views.Where(v => v.VideoId == id);
                    _context.Views.RemoveRange(views);

                    var watchLater = await _context.WatchLaterItems.SingleOrDefaultAsync(w => w.VideoId == id);
                    if (watchLater != null)
                    {
                        _context.WatchLaterItems.Remove(watchLater);
                    }

                    var watchHistory = await _context.WatchHistories.SingleOrDefaultAsync(w => w.VideoId == id);
                    if (watchHistory != null)
                    {
                        _context.WatchHistories.Remove(watchHistory);
                    }

                    var videoFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "videos", video.VideoPath);
                    if (File.Exists(videoFilePath))
                    {
                        File.Delete(videoFilePath);
                    }

                    var posterFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "posters", video.PosterPath);
                    if (File.Exists(posterFilePath))
                    {
                        File.Delete(posterFilePath);
                    }

                    _context.Videos.Remove(video);
                    await _context.SaveChangesAsync();
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public async Task<IEnumerable<Video>> GetVideosByChannelId(Guid channelId)
        {
            return await _context.Videos
                .Include(v => v.Category)
                .Include(v => v.Channel)
                .Include(v => v.Views)
                .Where(v => v.ChannelId == channelId)
                .ToListAsync();
        }
    }
}
