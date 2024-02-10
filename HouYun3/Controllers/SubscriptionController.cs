using HouYun3.IRepositories;
using HouYun3.Models;
using Microsoft.AspNetCore.Mvc;

namespace HouYun3.Controllers
{
    public class SubscriptionController : Controller
    {
        private readonly ISubscriptionRepository _subscriptionRepository;

        public SubscriptionController(ISubscriptionRepository subscriptionRepository)
        {
            _subscriptionRepository = subscriptionRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Subscribe(Guid channelId, string userId)
        {
            var existingSubscription = await _subscriptionRepository.GetSubscriptionByChannelAndUser(channelId, userId);
            if (existingSubscription != null)
            {
                return Ok();
            }

            var subscription = new Subscription
            {
                ChannelId = channelId,
                UserId = userId,
            };

            await _subscriptionRepository.CreateSubscription(subscription);

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Unsubscribe(Guid subscriptionId)
        {
            var subscription = await _subscriptionRepository.GetSubscriptionById(subscriptionId);
            if (subscription == null)
            {
                return Ok();
            }

            await _subscriptionRepository.DeleteSubscription(subscriptionId);

            return Ok();
        }
    }
}
