using HouYun3.Models;

namespace HouYun3.IRepositories
{
    public interface IChannelRepository
    {
        Task<Channel> GetChannelById(Guid id);
        Task<IEnumerable<Channel>> GetAllChannels();
        Task CreateChannel(Channel channel);
        Task UpdateChannel(Channel channel);
        Task DeleteChannel(Guid id);
        Task<Guid> GetChannelIdByUserId(string userId);
        Task<Channel> GetChannelByUserId(string userId);
        Task<Channel> GetChannelByName(string channelName);
        Task AddVideo(Video video, IFormFile videoFile, IFormFile posterFile);
        Task<Video> UpdateVideo(Video video);
        Task DeleteVideo(Guid id);
        Task<IEnumerable<Video>> GetVideosByChannelId(Guid channelId);
    }
}
