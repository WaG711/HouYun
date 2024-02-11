using HouYun3.IRepositories;
using HouYun3.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
        public async Task<IActionResult> Subscribe(Guid channelId)
        {
            string refererUrl = Request.Headers.Referer.ToString();
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

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

        [HttpPost]
        public async Task<IActionResult> Unsubscribe(Guid channelId)
        {
            string refererUrl = Request.Headers.Referer.ToString();

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var userSubscriptions = await _subscriptionRepository.GetSubscriptionsByUserId(userId);

            var subscriptionToUnsubscribe = userSubscriptions.FirstOrDefault(sub => sub.ChannelId == channelId);

            if (subscriptionToUnsubscribe == null)
            {
                return Redirect(refererUrl);
            }

            var subscriptionId = subscriptionToUnsubscribe.SubscriptionId;

            await _subscriptionRepository.DeleteSubscription(subscriptionId);

            return Redirect(refererUrl);
        }
    }
}
