using HouYun3.Models;

namespace HouYun3.IRepositories
{
    public interface ICommentRepository
    {
        Task<Comment> GetCommentByIdAsync(int id);
        Task<List<Comment>> GetAllCommentsAsync();
        Task AddCommentAsync(Comment comment);
        Task UpdateCommentAsync(Comment comment);
        Task DeleteCommentAsync(int id);
    }
}
