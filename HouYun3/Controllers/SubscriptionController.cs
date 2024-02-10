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

        [HttpGet]
        public async Task<IActionResult> Subscribe(Guid channelId, string userId)
        {
            string refererUrl = Request.Headers.Referer.ToString();

            var existingSubscription = await _subscriptionRepository.GetSubscriptionByChannelAndUser(channelId, userId);
            if (existingSubscription != null)
            {
                return Redirect(refererUrl);
            }

            var subscription = new Subscription
            {
                ChannelId = channelId,
                UserId = userId,
            };

            await _subscriptionRepository.CreateSubscription(subscription);

            return Redirect(refererUrl);
        }

        [HttpGet]
        public async Task<IActionResult> Unsubscribe(Guid subscriptionId)
        {
            string refererUrl = Request.Headers.Referer.ToString();

            var subscription = await _subscriptionRepository.GetSubscriptionById(subscriptionId);
            if (subscription == null)
            {
                return Redirect(refererUrl);
            }

            await _subscriptionRepository.DeleteSubscription(subscriptionId);

            return Redirect(refererUrl);
        }
    }
}
