using HouYun3.Models;

namespace HouYun3.IRepositories
{
    public interface ICommentRepository
    {
        Task<List<Comment>> GetComments(int videoId);
        Task<Comment> GetComment(int commentId);
        Task AddComment(Comment comment);
        Task UpdateComment(Comment comment);
        Task DeleteComment(int commentId);
    }
}
