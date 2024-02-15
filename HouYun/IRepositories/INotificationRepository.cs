using HouYun.Models;

namespace HouYun.IRepositories
{
    public interface INotificationRepository
    {
        Task<IEnumerable<Notification>> GetAllNotificationsByChannelId(Guid channelId);
        Task AddNotification(Notification notification);
        Task<Notification> UpdateNotification(Notification notification);
        Task DeleteReadNotifications();
    }
}
