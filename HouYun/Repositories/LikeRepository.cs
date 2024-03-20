using HouYun.IRepositories;
using HouYun.Models;
using HouYun.Data;
using Microsoft.EntityFrameworkCore;

namespace HouYun.Repositories
{
    public class LikeRepository : ILikeRepository
    {
        private readonly ApplicationDbContext _context;

        public LikeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Video>> GetChannelLikedVideos(Guid channelId)
        {
            return await _context.Likes
                .Where(l => l.ChannelId == channelId)
                .OrderByDescending(l => l.LikeDate)
                .Include(l => l.Video)
                    .ThenInclude(v => v.Channel)
                    .ThenInclude(v => v.Views)
                .Select(l => l.Video)
                .ToListAsync();
        }

        public async Task<Like> GetLikeByChannelIdAndVideoId(Guid channelId, Guid videoId)
        {
            return await _context.Likes
                .FirstOrDefaultAsync(l => l.ChannelId == channelId && l.VideoId == videoId);
        }

        public async Task<Like> AddLike(Like like)
        {
            _context.Likes.Add(like);
            await _context.SaveChangesAsync();
            return like;
        }

        public async Task DeleteLike(Guid id)
        {
            var like = await _context.Likes.FindAsync(id);

            if (like != null)
            {
                _context.Likes.Remove(like);
                await _context.SaveChangesAsync();
            }
        }
    }
}
