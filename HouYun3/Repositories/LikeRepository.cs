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

        public async Task<int> GetLikesCountByVideoId(int videoId)
        {
            return await _context.Likes.CountAsync(l => l.VideoId == videoId);
        }

        public async Task<bool> IsUserLikedVideo(int videoId, int userId)
        {
            return await _context.Likes.AnyAsync(l => l.VideoId == videoId && l.User.UserId == userId);
        }

        public async Task AddLike(Like like)
        {
            _context.Likes.Add(like);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveLike(int likeId)
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
