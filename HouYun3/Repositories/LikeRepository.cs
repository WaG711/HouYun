using HouYun3.IRepositories;
using HouYun3.Models;
using HouYun3.Data;
using Microsoft.EntityFrameworkCore;

namespace HouYun3.Repositories
{
    public class LikeRepository : ILikeRepository
    {
        private readonly HouYun3Context _context;

        public LikeRepository(HouYun3Context context)
        {
            _context = context;
        }

        public async Task<List<Like>> GetLikes(int videoId)
        {
            return await _context.Likes
                .Where(l => l.VideoID == videoId)
                .Include(l => l.User)
                .ToListAsync();
        }

        public async Task<Like> GetLike(int likeId)
        {
            return await _context.Likes
                .Include(l => l.User)
                .FirstOrDefaultAsync(l => l.LikeID == likeId);
        }

        public async Task AddLike(Like like)
        {
            _context.Likes.Add(like);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteLike(int likeId)
        {
            var like = await _context.Likes.FindAsync(likeId);
            if (like != null)
            {
                _context.Likes.Remove(like);
                await _context.SaveChangesAsync();
            }
        }
    }
}
