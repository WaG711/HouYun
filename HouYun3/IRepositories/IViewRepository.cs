using HouYun2.Models;

namespace HouYun2.IRepositories
{
    public interface IViewRepository
    {
        Task<List<View>> GetViews(int videoId);
        Task AddView(View view);
        Task DeleteView(int viewId);

    }
}
