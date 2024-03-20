using HouYun.Models;
using HouYun.ViewModels.forUser;

namespace HouYun.IRepositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllUsers();
        Task<string> GetUserNameById(string userId);
        Task DeleteUser(string id);
        Task ChangeRoles(string id, string roleName);
        Task<bool> ChangeUserName(string userId, string newUserName, string Password);
        Task<bool> ChangeUserPassword(string userId, string OldPassword, string NewPassword);
        Task<bool> LoginUser(string userName, string password, bool rememberMe);
        Task<bool> RegistrationUser(RegistrationViewModel model);
        Task Logout();
    }
}
