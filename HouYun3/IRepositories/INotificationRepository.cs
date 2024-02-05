using HouYun3.Models;

namespace HouYun3.IRepositories
{
    public interface INotificationRepository
    {
        Task<IEnumerable<Notification>> GetUnreadNotificationsByUserId(int userId);
        Task<int> GetUnreadNotificationsCountByUserId(int userId);
        Task AddNotification(Notification notification);
        Task MarkNotificationAsRead(int notificationId);
    }
}
