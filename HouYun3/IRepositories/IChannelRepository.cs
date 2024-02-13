using HouYun3.Models;

namespace HouYun3.IRepositories
{
    public interface IChannelRepository
    {
        Task<Channel> GetChannelById(Guid id);
        Task<IEnumerable<Channel>> GetAllChannels();
        Task CreateChannel(Channel channel);
        Task UpdateChannel(Guid СhannelId, string newName, string newDescription);
        Task DeleteChannel(Guid id);
        Task<Guid> GetChannelIdByUserId(string userId);
        Task<Channel> GetChannelByUserId(string userId);
        Task<Channel> GetChannelByName(string channelName);
    }
}
