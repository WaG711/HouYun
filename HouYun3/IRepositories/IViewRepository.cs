using HouYun3.Models;

namespace HouYun3.IRepositories
{
    public interface IViewRepository
    {
        Task<IEnumerable<View>> GetAllViews();
        Task<View> GetViewById(int viewId);
        Task AddView(View view);
        Task DeleteView(int viewId);
    }
}
