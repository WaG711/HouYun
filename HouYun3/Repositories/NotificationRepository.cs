using HouYun2.IRepositories;
using HouYun2.Models;
using HouYun3.Data;
using Microsoft.EntityFrameworkCore;

namespace HouYun2.Repositories
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly HouYun3Context _context;

        public NotificationRepository(HouYun3Context context)
        {
            _context = context;
        }

        public async Task<List<Notification>> GetNotifications(int userId)
        {
            return await _context.Notifications
                .Where(n => n.UserID == userId)
                .ToListAsync();
        }

        public async Task<Notification> GetNotification(int notificationId)
        {
            return await _context.Notifications
                .FirstOrDefaultAsync(n => n.NotificationID == notificationId);
        }

        public async Task AddNotification(Notification notification)
        {
            _context.Notifications.Add(notification);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteNotification(int notificationId)
        {
            var notification = await _context.Notifications.FindAsync(notificationId);
            if (notification != null)
            {
                _context.Notifications.Remove(notification);
                await _context.SaveChangesAsync();
            }
        }
    }
}
