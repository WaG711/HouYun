using HouYun.IRepositories;
using HouYun.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HouYun.Controllers
{
    [Authorize(Roles = "Admin,User")]
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
            var subscribedChannels = await _subscriptionRepository.GetUserSubscribedVideos(userId);

            return View(subscribedChannels);
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

            await _subscriptionRepository.DeleteSubscription(subscriptionToUnsubscribe.SubscriptionId);

            return Redirect(refererUrl);
        }

        public async Task<IActionResult> SubscribedChannels(string userId)
        {
            var subscribedChannels = await _subscriptionRepository.GetUserSubscribedChannels(userId);
            var userNicknames = subscribedChannels.Select(channel => channel.Name);
            return View(userNicknames);
        }
    }
}
