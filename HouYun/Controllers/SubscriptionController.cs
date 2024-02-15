﻿using HouYun.IRepositories;
using HouYun.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HouYun.Controllers
{
    [Authorize(Roles = "Admin")]
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

            await _subscriptionRepository.DeleteSubscription(subscriptionToUnsubscribe.SubscriptionId);

            return Redirect(refererUrl);
        }
    }
}