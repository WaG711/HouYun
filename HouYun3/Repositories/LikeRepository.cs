using HouYun3.IRepositories;
using HouYun3.Models;
using HouYun3.Data;
using Microsoft.EntityFrameworkCore;

namespace HouYun3.Repositories
{
    public class LikeRepository : ILikeRepository
    {
        private readonly ApplicationDbContext _context;

        public LikeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Like> GetLikeByChannelIdAndVideoId(Guid channelId, Guid videoId)
        {
            return await _context.Likes.FirstOrDefaultAsync(v => v.ChannelId == channelId && v.VideoId == videoId);
        }

        public async Task<IEnumerable<Like>> GetAllLikes()
        {
            return await _context.Likes.ToListAsync();
        }

        public async Task<Like> GetLikeById(Guid id)
        {
            return await _context.Likes.FindAsync(id);
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
