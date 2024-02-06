using HouYun3.Models;

namespace HouYun3.IRepositories
{
    public interface INotificationRepository
    {
        Task<Notification> GetNotificationByIdAsync(int id);
        Task<List<Notification>> GetAllNotificationsAsync();
        Task AddNotificationAsync(Notification notification);
        Task UpdateNotificationAsync(Notification notification);
        Task DeleteNotificationAsync(int id);
    }
}
