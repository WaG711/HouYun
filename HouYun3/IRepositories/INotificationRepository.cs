using HouYun3.Models;

namespace HouYun3.IRepositories
{
    public interface INotificationRepository
    {
        Task<IEnumerable<Notification>> GetUnreadNotificationsByUserId(string userId);
        Task<int> GetUnreadNotificationsCountByUserId(string userId);
        Task AddNotification(Notification notification);
        Task MarkNotificationAsRead(int notificationId);
    }
}
