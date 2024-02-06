using HouYun3.Models;
using Microsoft.AspNetCore.Identity;

namespace HouYun3.IRepositories
{
    public interface IUserRepository
    {
        Task<User> GetUserByIdAsync(string id);
        Task<List<User>> GetAllUsersAsync();
        Task<IdentityResult> AddUserAsync(User user, string password);
        Task<IdentityResult> UpdateUserAsync(User user);
        Task<IdentityResult> DeleteUserAsync(string id);
    }
}
