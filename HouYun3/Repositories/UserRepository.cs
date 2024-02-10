using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using HouYun3.IRepositories;
using HouYun3.Models;
using Azure.Identity;
using System.Security.Claims;
using HouYun3.ViewModels.forUser;


namespace HouYun3.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public UserRepository(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;

        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await _userManager.Users.ToListAsync();
        }

        public async Task<User> GetUserById(string id)
        {
            return await _userManager.FindByIdAsync(id);
        }

        public async Task<User> AddUser(User user)
        {
            var result = await _userManager.CreateAsync(user);
            if (result.Succeeded)
            {
                return user;
            }
            return null;
        }

        public async Task<User> UpdateUser(User user)
        {
            var existingUser = await _userManager.FindByIdAsync(user.Id);
            if (existingUser != null)
            {
                existingUser.Email = user.Email;
                existingUser.UserName = user.UserName;

                var result = await _userManager.UpdateAsync(existingUser);
                if (result.Succeeded)
                {
                    return existingUser;
                }
            }
            return null;
        }

        public async Task DeleteUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                await _userManager.DeleteAsync(user);
            }
        }

        public async Task<bool> ChangeUserPassword(string userId, string oldPassword, string newPassword)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return false; 
            }

            var result = await _userManager.ChangePasswordAsync(user, oldPassword, newPassword);

            return result.Succeeded;
        }

        public async Task<bool> LoginUser(string userName, string password, bool rememberMe)
        {
            var result = await _signInManager.PasswordSignInAsync(userName, password, rememberMe, false);
            return result.Succeeded;
        }

        public async Task<bool> RegisterUser(RegisterViewModel model)
        {
            var user = new User { Email = model.Email, UserName = model.UserName, Channel = { Name = model.UserName } };
            var existingUser = await _userManager.FindByEmailAsync(model.Email);

            if (existingUser != null)
            {
                return false;
            }

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: false);
                return true;
            }

            return false;
        }

        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
