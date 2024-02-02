using HouYun3.Models;

namespace HouYun3.IRepositories
{
    public interface IViewRepository
    {
        Task<List<View>> GetViews(int videoId);
        Task AddView(View view);
        Task DeleteView(int viewId);

    }
}
