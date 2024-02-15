using HouYun3.IRepositories;
using HouYun3.Models;
using HouYun3.Data;
using Microsoft.EntityFrameworkCore;

namespace HouYun3.Repositories
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ISubscriptionRepository _subscriptionRepository;

        public NotificationRepository(ApplicationDbContext context, ISubscriptionRepository subscriptionRepository)
        {
            _context = context;
            _subscriptionRepository = subscriptionRepository;
        }

        public async Task<IEnumerable<Notification>> GetAllNotificationsByChannelId(Guid channelId)
        {
            return await _context.Notifications
                .Where(c => c.ChannelId == channelId)
                .OrderByDescending(n => n.NotificationDate)
                .ToListAsync();
        }

        public async Task<Notification> GetNotificationById(Guid id)
        {
            return await _context.Notifications.FindAsync(id);
        }

        public async Task AddNotification(Notification notification)
        {
            var subscribers = await _subscriptionRepository.GetSubscriptionsByChannelId(notification.ChannelId);
            foreach (var subscriber in subscribers)
            {
                var sub = subscriber.User.Channel.ChannelId;
                var notify = new Notification
                {
                    Message = notification.Message,
                    ChannelId = sub
                };
                _context.Notifications.Add(notify);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Notification> UpdateNotification(Notification notification)
        {
            notification.IsRead = true;
            _context.Entry(notification).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return notification;
        }

        public async Task DeleteReadNotifications()
        {
            var notifications = await _context.Notifications.Where(n => n.IsRead).ToListAsync();

            if (notifications.Count != 0)
            {
                _context.Notifications.RemoveRange(notifications);
                await _context.SaveChangesAsync();
            }
        }
    }
}
