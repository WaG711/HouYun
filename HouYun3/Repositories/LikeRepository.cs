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

        public async Task<Like> GetLikeByIdAsync(int id)
        {
            return await _context.Likes.FindAsync(id);
        }

        public async Task<List<Like>> GetAllLikesAsync()
        {
            return await _context.Likes.ToListAsync();
        }

        public async Task AddLikeAsync(Like like)
        {
            _context.Likes.Add(like);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteLikeAsync(int id)
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
