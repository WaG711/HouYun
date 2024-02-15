using HouYun3.Data;
using HouYun3.IRepositories;
using HouYun3.Models;
using Microsoft.EntityFrameworkCore;

namespace HouYun3.Repositories
{
    public class SubscriptionRepository : ISubscriptionRepository
    {
        private readonly ApplicationDbContext _context;

        public SubscriptionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Subscription> GetSubscriptionByChannelAndUser(Guid channelId, string userId)
        {
            return await _context.Subscriptions
                .FirstOrDefaultAsync(s => s.ChannelId == channelId && s.UserId == userId);
        }

        public async Task<IEnumerable<Subscription>> GetSubscriptionsByUserId(string userId)
        {
            return await _context.Subscriptions
                .Where(s => s.UserId == userId).ToListAsync();
        }

        public async Task<IEnumerable<Subscription>> GetSubscriptionsByChannelId(Guid channelId)
        {
            var channel = await _context.Channels
                .Include(c => c.Subscribers)
                    .ThenInclude(f => f.User.Channel)
                .FirstOrDefaultAsync(c => c.ChannelId == channelId);

            return channel.Subscribers;
        }


        public async Task CreateSubscription(Subscription subscription)
        {
            await _context.Subscriptions.AddAsync(subscription);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteSubscription(Guid id)
        {
            var subscription = await _context.Subscriptions.FindAsync(id);

            if (subscription != null)
            {
                _context.Subscriptions.Remove(subscription);
                await _context.SaveChangesAsync();
            }
        }
    }
}
