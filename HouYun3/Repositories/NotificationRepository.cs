using HouYun3.IRepositories;
using HouYun3.Models;
using HouYun3.Data;
using Microsoft.EntityFrameworkCore;

namespace HouYun3.Repositories
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly HouYun3Context _context;

        public NotificationRepository(HouYun3Context context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Notification>> GetUnreadNotificationsByUserId(string userId)
        {
            return await _context.Notifications
                .Where(n => n.User.Id == userId && !n.IsRead)
                .OrderByDescending(n => n.NotificationDate)
                .ToListAsync();
        }

        public async Task<int> GetUnreadNotificationsCountByUserId(string userId)
        {
            return await _context.Notifications.CountAsync(n => n.User.Id == userId && !n.IsRead);
        }

        public async Task AddNotification(Notification notification)
        {
            _context.Notifications.Add(notification);
            await _context.SaveChangesAsync();
        }

        public async Task MarkNotificationAsRead(int notificationId)
        {
            var notification = await _context.Notifications.FindAsync(notificationId);
            if (notification != null)
            {
                notification.IsRead = true;
                await _context.SaveChangesAsync();
            }
        }
    }
}
