﻿using HouYun.Models;

namespace HouYun.IRepositories
{
    public interface ISubscriptionRepository
    {
        Task<IEnumerable<Subscription>> GetSubscriptionsByUserId(string userId);
        Task<IEnumerable<Channel>> GetUserSubscribedChannels(string userId);
        Task<IEnumerable<Video>> GetUserSubscribedVideos(string userId);
        Task CreateSubscription(Subscription subscription);
        Task DeleteSubscription(Guid id);
        Task<Subscription> GetSubscriptionByChannelAndUser(Guid channelId, string userId);
        Task<IEnumerable<Subscription>> GetSubscriptionsByChannelId(Guid channelId);
    }
}
