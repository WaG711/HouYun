using HouYun3.Models;

namespace HouYun3.IRepositories
{
    public interface ISubscriptionRepository
    {
        Task<Subscription> GetSubscriptionById(Guid id);
        Task<IEnumerable<Subscription>> GetAllSubscriptions();
        Task CreateSubscription(Subscription subscription);
        Task UpdateSubscription(Subscription subscription);
        Task DeleteSubscription(Guid id);
        Task<Subscription> GetSubscriptionByChannelAndUser(Guid channelId, string userId);
    }
}
