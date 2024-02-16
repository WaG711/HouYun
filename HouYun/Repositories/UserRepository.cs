using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using HouYun.IRepositories;
using HouYun.Models;
using HouYun.ViewModels.forUser;


namespace HouYun.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserRepository(UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await _userManager.Users.ToListAsync();
        }

        public async Task DeleteUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                await _userManager.DeleteAsync(user);
            }
        }

        public async Task<bool> ChangeUsername(string userId, string newUsername, string oldPassword)
        {
            var user = await _userManager.FindByIdAsync(userId);

            var passwordValid = await _userManager.CheckPasswordAsync(user, oldPassword);
            if (!passwordValid)
            {
                return false;
            }

            var existingUser = await _userManager.FindByNameAsync(newUsername);
            if (existingUser != null && existingUser.Id != userId)
            {
                return false;
            }

            await _userManager.SetUserNameAsync(user, newUsername);

            return true;
        }

        public async Task<bool> ChangeUserPassword(string userId, string oldPassword, string newPassword)
        {
            var user = await _userManager.FindByIdAsync(userId);

            var result = await _userManager.ChangePasswordAsync(user, oldPassword, newPassword);

            return result.Succeeded;
        }

        public async Task<bool> LoginUser(string userName, string password, bool rememberMe)
        {
            var result = await _signInManager.PasswordSignInAsync(userName, password, rememberMe, false);
            return result.Succeeded;
        }

        public async Task<bool> RegistrationUser(RegisterViewModel model)
        {
            var existingUser = await _userManager.FindByEmailAsync(model.Email);

            if (existingUser != null)
            {
                return false;
            }

            var user = new User
            {
                Email = model.Email,
                UserName = model.UserName,
                Channel =
                {
                    Name = model.UserName,
                    Description = $"This is the {model.UserName} user's channel"
                }
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                return false;
            }

            await ManageRoles(user);

            await _signInManager.SignInAsync(user, isPersistent: false);
            return true;
        }

        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
        }

        private async Task ManageRoles(User user)
        {
            if (!_roleManager.RoleExistsAsync("Admin").GetAwaiter().GetResult())
            {
                await _roleManager.CreateAsync(new IdentityRole("Admin"));
                await _roleManager.CreateAsync(new IdentityRole("User"));
            }

            var role = (user.Email == "nikitanik10305@gmail.com" || user.Email == "rupcyes@mail.com") ? "Admin" : "User";
            await _userManager.AddToRoleAsync(user, role);
        }
    }
}
