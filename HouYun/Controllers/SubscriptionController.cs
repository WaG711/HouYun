using HouYun.IRepositories;
using HouYun.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HouYun.Controllers
{
    [Authorize(Roles = "Admin,User,Author")]
    public class SubscriptionController : Controller
    {
        private readonly ISubscriptionRepository _subscriptionRepository;

        public SubscriptionController(ISubscriptionRepository subscriptionRepository)
        {
            _subscriptionRepository = subscriptionRepository;
        }

        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var subscribedVideos = await _subscriptionRepository.GetUserSubscribedVideos(userId);

            return View(subscribedVideos);
        }

        public async Task<IActionResult> SubscribedChannels()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var subscribedChannels = await _subscriptionRepository.GetUserSubscribedChannels(userId);

            return PartialView("_SubscribedChannelsPartial", subscribedChannels);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Subscribe(Guid channelId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

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
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Unsubscribe(Guid channelId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userSubscriptions = await _subscriptionRepository.GetSubscriptionsByUserId(userId);

            var subscriptionToUnsubscribe = userSubscriptions.FirstOrDefault(sub => sub.ChannelId == channelId);
            if (subscriptionToUnsubscribe == null)
            {
                return Ok();
            }

            await _subscriptionRepository.DeleteSubscription(subscriptionToUnsubscribe.SubscriptionId);

            return Ok();
        }
    }
}
