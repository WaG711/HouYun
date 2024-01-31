using HouYun2.Models;

namespace HouYun2.IRepositories
{
    public interface INotificationRepository
    {
        Task<List<Notification>> GetNotifications(int userId);
        Task<Notification> GetNotification(int notificationId);
        Task AddNotification(Notification notification);
        Task DeleteNotification(int notificationId);
    }
}
