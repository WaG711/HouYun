using HouYun.Models;

namespace HouYun.IRepositories
{
    public interface IViewRepository
    {
        Task AddView(View view);
        Task<View> GetViewByVideoAndChannel(Guid videoId, Guid channelId);
    }
}
