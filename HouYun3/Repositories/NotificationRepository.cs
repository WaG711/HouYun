using HouYun3.IRepositories;
using HouYun3.Models;
using HouYun3.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Channels;

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
            var notifications = await _context.Notifications
            .Include(c => c.Channel)
            .Where(c => c.ChannelId == channelId)
            .OrderByDescending(w => w.NotificationDate)
            .ToListAsync();

            foreach (var notification in notifications)
            {
                notification.IsRead = true;
            }

            await _context.SaveChangesAsync();

            return notifications;
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
            _context.Entry(notification).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return notification;
        }

        public async Task DeleteNotification(Guid id)
        {
            var notification = await _context.Notifications.FindAsync(id);
            if (notification != null)
            {
                _context.Notifications.Remove(notification);
                await _context.SaveChangesAsync();
            }
        }
    }
}
