using HouYun3.Models;

namespace HouYun3.IRepositories
{
    public interface IChannelRepository
    {
        Task<IEnumerable<Channel>> GetAllChannels();
        Task UpdateChannel(Channel channel);
        Task DeleteChannel(Guid id);
        Task<Guid> GetChannelIdByUserId(string userId);
        Task<Channel> GetChannelByUserId(string userId);
        Task<Channel> GetChannelByName(string channelName);
    }
}
