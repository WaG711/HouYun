using HouYun3.IRepositories;
using HouYun3.Models;
using HouYun3.Data;

namespace HouYun3.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDbContext _context;

        public CommentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Comment> AddComment(Comment comment)
        {
            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();
            return comment;
        }
    }
}
