using HouYun3.Models;

namespace HouYun3.IRepositories
{
    public interface IViewRepository
    {
        Task AddView(View view);
        Task<View> GetViewByVideoAndChannel(Guid videoId, Guid channelId);
    }
}
