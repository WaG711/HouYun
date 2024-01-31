using HouYun2.IRepositories;
using HouYun2.Models;
using HouYun3.Data;
using Microsoft.EntityFrameworkCore;

namespace HouYun2.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly HouYun3Context _context;

        public CommentRepository(HouYun3Context context)
        {
            _context = context;
        }

        public async Task<List<Comment>> GetComments(int videoId)
        {
            return await _context.Comments
                .Where(c => c.VideoID == videoId)
                .Include(c => c.User)
                .ToListAsync();
        }

        public async Task<Comment> GetComment(int commentId)
        {
            return await _context.Comments
                .Include(c => c.User)
                .FirstOrDefaultAsync(c => c.CommentID == commentId);
        }

        public async Task AddComment(Comment comment)
        {
            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateComment(Comment comment)
        {
            _context.Entry(comment).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteComment(int commentId)
        {
            var comment = await _context.Comments.FindAsync(commentId);
            if (comment != null)
            {
                _context.Comments.Remove(comment);
                await _context.SaveChangesAsync();
            }
        }
    }
}
