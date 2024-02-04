using HouYun3.Models;

namespace HouYun3.IRepositories
{
    public interface ICommentRepository
    {
        Task<IEnumerable<Comment>> GetCommentsByVideoId(int videoId);
        Task<Comment> GetCommentById(int commentId);
        Task AddComment(Comment comment);
        Task UpdateComment(Comment comment);
        Task DeleteComment(int commentId);
    }
}
