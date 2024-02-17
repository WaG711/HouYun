﻿using HouYun.Models;
using HouYun.ViewModels.forUser;

namespace HouYun.IRepositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllUsers();
        Task DeleteUser(string id);
        Task<bool> ChangeUsername(string userId, string newUsername, string Password);
        Task<bool> ChangeUserPassword(string userId, string OldPassword, string NewPassword);
        Task<bool> LoginUser(string userName, string password, bool rememberMe);
        Task<bool> RegistrationUser(RegisterViewModel model);
        Task Logout();
        Task<string> GetUsernameById(string userId);
    }
}
