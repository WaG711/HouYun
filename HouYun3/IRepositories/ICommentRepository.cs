using HouYun3.Models;

namespace HouYun3.IRepositories
{
    public interface ICommentRepository
    {
        Task<Comment> AddComment(Comment comment);
    }
}
