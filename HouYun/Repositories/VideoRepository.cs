using HouYun.IRepositories;
using HouYun.Models;
using HouYun.Data;
using Microsoft.EntityFrameworkCore;

namespace HouYun.Repositories
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
                .Include(v => v.Channel)
                .Include(v => v.Views)
                .OrderByDescending(v => v.UploadDate)
                .ToListAsync();

            var videosExceptId = allVideos.Where(v => v.VideoId != id).ToList();

            return videosExceptId;
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

        public async Task<Video> GetVideoById(Guid id)
        {
            return await _context.Videos
                .Include(v => v.Category)
                .Include(v => v.Channel)
                    .ThenInclude(c => c.Subscribers)
                .Include(v => v.Comments)
                    .ThenInclude(c => c.Channel)
                .Include(v => v.Likes)
                .Include(v => v.Views)
                .SingleOrDefaultAsync(v => v.VideoId == id);
        }

        public async Task AddVideo(Video video, IFormFile videoFile, IFormFile posterFile)
        {
            var videoFileName = await SaveFile(videoFile, "videos");
            var posterFileName = await SaveFile(posterFile, "posters");

            try
            {
                video.VideoPath = videoFileName;
                video.PosterPath = posterFileName;

                _context.Videos.Add(video);
                await _context.SaveChangesAsync();

                var notification = new Notification()
                {
                    ChannelId = video.ChannelId,
                    VideoId = video.VideoId
                };
                await _notificationRepository.AddNotification(notification);
            }
            catch (Exception)
            {
                await DeleteFile(videoFileName, "videos");
                await DeleteFile(posterFileName, "posters");
                throw;
            }
        }

        public async Task DeleteVideo(Guid id)
        {
            var video = await _context.Videos.FindAsync(id);

            try
            {
                await DeleteNotifications(id);
                await DeleteComments(id);
                await DeleteLikes(id);
                await DeleteViews(id);
                await DeleteWatchLaterItem(id);
                await DeleteWatchHistory(id);

                await DeleteFile(video.VideoPath, "videos");
                await DeleteFile(video.PosterPath, "posters");

                _context.Videos.Remove(video);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private async Task<string> SaveFile(IFormFile file, string folderName)
        {
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", folderName, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return fileName;
        }

        private async Task DeleteFile(string fileName, string folderName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                return;
            }

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", folderName, fileName);

            if (File.Exists(filePath))
            {
                await Task.Run(() => File.Delete(filePath));
            }
        }

        private async Task DeleteNotifications(Guid id)
        {
            var notifications = await _context.Notifications.Where(n => n.VideoId == id).ToListAsync();
            _context.Notifications.RemoveRange(notifications);
        }

        private async Task DeleteComments(Guid id)
        {
            var comments = await _context.Comments.Where(c => c.VideoId == id).ToListAsync();
            _context.Comments.RemoveRange(comments);
        }

        private async Task DeleteLikes(Guid id)
        {
            var likes = await _context.Likes.Where(l => l.VideoId == id).ToListAsync();
            _context.Likes.RemoveRange(likes);
        }

        private async Task DeleteViews(Guid id)
        {
            var views = await _context.Views.Where(v => v.VideoId == id).ToListAsync();
            _context.Views.RemoveRange(views);
        }

        private async Task DeleteWatchLaterItem(Guid id)
        {
            var watchLater = await _context.WatchLaterItems.FirstOrDefaultAsync(w => w.VideoId == id);
            if (watchLater != null)
            {
                _context.WatchLaterItems.Remove(watchLater);
            }
        }

        private async Task DeleteWatchHistory(Guid id)
        {
            var watchHistory = await _context.WatchHistories.FirstOrDefaultAsync(w => w.VideoId == id);
            if (watchHistory != null)
            {
                _context.WatchHistories.Remove(watchHistory);
            }
        }
    }
}
