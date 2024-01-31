using HouYun2.Models;

namespace HouYun2.IRepositories
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllUsers();
        Task<User> GetUser(int userId);
        Task AddUser(User user);
        Task UpdateUser(User user);
        Task DeleteUser(int userId);
    }
}
