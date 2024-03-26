using HouYun.IRepositories;
using HouYun.Models;
using HouYun.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.SignalR;

namespace HouYun.Repositories
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
                .Include(n => n.Video)
                    .ThenInclude(v => v.Channel)
                .Where(n => n.ChannelId == channelId)
                .OrderByDescending(n => n.NotificationDate)
                .ToListAsync();
        }

        public async Task AddNotification(Notification notification)
        {
            if (notification.Message == null)
            {
                var subscribers = await _subscriptionRepository.GetSubscriptionsByChannelId(notification.ChannelId);
                foreach (var subscriber in subscribers)
                {
                    var notify = new Notification
                    {
                        ChannelId = subscriber.User.Channel.ChannelId,
                        VideoId = notification.VideoId
                    };

                    _context.Notifications.Add(notify);
                    await _context.SaveChangesAsync();
                }
            }
            else
            {
                _context.Notifications.Add(notification);
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
            var notificationsGroupedByChannel = await _context.Notifications
                                                    .Where(n => n.IsRead)
                                                    .GroupBy(n => n.ChannelId)
                                                    .ToListAsync();

            foreach (var group in notificationsGroupedByChannel)
            {
                var lastTenNotifications = group.OrderByDescending(n => n.NotificationDate).Take(10);

                var notificationsToRemove = group.Except(lastTenNotifications);

                _context.Notifications.RemoveRange(notificationsToRemove);
            }

            await _context.SaveChangesAsync();
        }
    }
}
