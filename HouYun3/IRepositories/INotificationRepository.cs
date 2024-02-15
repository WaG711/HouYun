using HouYun3.Models;

namespace HouYun3.IRepositories
{
    public interface INotificationRepository
    {
        Task<IEnumerable<Notification>> GetAllNotificationsByChannelId(Guid channelId);
        Task<Notification> GetNotificationById(Guid id);
        Task AddNotification(Notification notification);
        Task<Notification> UpdateNotification(Notification notification);
        Task DeleteReadNotifications();
    }
}
