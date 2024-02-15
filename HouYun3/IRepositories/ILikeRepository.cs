using HouYun3.Models;

namespace HouYun3.IRepositories
{
    public interface ILikeRepository
    {
        Task<Like> GetLikeByChannelIdAndVideoId(Guid channelId, Guid videoId);
        Task<Like> AddLike(Like like);
        Task DeleteLike(Guid id);
    }
}
