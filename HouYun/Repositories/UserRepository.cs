using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using HouYun.IRepositories;
using HouYun.Models;
using HouYun.ViewModels.forUser;
using HouYun.Data;


namespace HouYun.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserRepository(UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _context = context;
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await _userManager.Users
                .Include(u => u.Channel)
                .ToListAsync();
        }

        public async Task DeleteUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user != null)
            {
                await DeleteChannel(user.Id);
                await _userManager.DeleteAsync(user);
            }
        }

        public async Task<bool> ChangeUserName(string userId, string newUserName, string oldPassword)
        {
            var user = await _userManager.FindByIdAsync(userId);

            var passwordValid = await _userManager.CheckPasswordAsync(user, oldPassword);
            if (!passwordValid)
            {
                return false;
            }

            var existingUser = await _userManager.FindByNameAsync(newUserName);
            if (existingUser != null && existingUser.Id != userId)
            {
                return false;
            }

            await _userManager.SetUserNameAsync(user, newUserName);

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

        public async Task<bool> RegistrationUser(RegistrationViewModel model)
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

        public async Task<string> GetUserNameById(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            return user.UserName;
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

        private async Task DeleteChannel(string id)
        {
            var channel = await _context.Channels.FirstOrDefaultAsync(c => c.UserId == id);
            if (channel != null)
            {
                _context.Channels.Remove(channel);
            }
        }
    }
}
