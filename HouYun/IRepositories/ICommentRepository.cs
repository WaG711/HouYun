using HouYun.Models;

namespace HouYun.IRepositories
{
    public interface ICommentRepository
    {
        Task<Comment> AddComment(Comment comment);
    }
}
