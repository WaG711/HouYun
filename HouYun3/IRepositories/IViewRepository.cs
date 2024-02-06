using HouYun3.Models;

namespace HouYun3.IRepositories
{
    public interface IViewRepository
    {
        Task<View> GetViewByIdAsync(int id);
        Task<List<View>> GetAllViewsAsync();
        Task AddViewAsync(View view);
        Task DeleteViewAsync(int id);
    }
}
