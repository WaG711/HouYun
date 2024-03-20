using HouYun.Models;

namespace HouYun.IRepositories
{
    public interface ILikeRepository
    {
        Task<IEnumerable<Video>> GetChannelLikedVideos(Guid channelId);
        Task<Like> GetLikeByChannelIdAndVideoId(Guid channelId, Guid videoId);
        Task<Like> AddLike(Like like);
        Task DeleteLike(Guid id);
    }
}
