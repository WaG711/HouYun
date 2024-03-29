﻿using HouYun.Models;

namespace HouYun.IRepositories
{
    public interface IChannelRepository
    {
        Task UpdateChannel(Channel channel);
        Task UpdateBannerChannel(Channel channel, IFormFile bannerFile);
        Task DeleteChannel(Channel channel);
        Task<Guid> GetChannelIdByUserId(string userId);
        Task<Channel> GetChannelByUserId(string userId);
        Task<Channel> GetChannelByName(string channelName);
    }
}
