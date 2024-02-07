using HouYun3.Models;

namespace HouYun3.IRepositories
{
    public interface ICommentRepository
    {
        Task<IEnumerable<Comment>> GetAllComments();
        Task<Comment> GetCommentById(Guid id);
        Task<Comment> AddComment(Comment comment);
        Task<Comment> UpdateComment(Comment comment);
        Task DeleteComment(Guid id);
    }
}
