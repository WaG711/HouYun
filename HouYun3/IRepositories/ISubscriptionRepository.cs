using HouYun3.Models;

namespace HouYun3.IRepositories
{
    public interface ISubscriptionRepository
    {
        Task<IEnumerable<Subscription>> GetSubscriptionsByUserId(string userId);
        Task CreateSubscription(Subscription subscription);
        Task DeleteSubscription(Guid id);
        Task<Subscription> GetSubscriptionByChannelAndUser(Guid channelId, string userId);
        Task<IEnumerable<Subscription>> GetSubscriptionsByChannelId(Guid channelId);
    }
}
