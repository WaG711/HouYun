using HouYun3.Models;

namespace HouYun3.IRepositories
{
    public interface IViewRepository
    {
        Task<IEnumerable<View>> GetAllViews();
        Task<View> GetViewById(Guid id);
        Task AddView(View view);
        Task DeleteView(Guid id);
        Task<View> GetViewByVideoAndUser(Guid videoId, string userId);
    }
}
