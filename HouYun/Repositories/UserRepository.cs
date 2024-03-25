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
        private readonly IChannelRepository _channelRepository;
        private readonly INotificationRepository _notificationRepository;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserRepository(UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<IdentityRole> roleManager,
            ApplicationDbContext context, IChannelRepository channelRepository, INotificationRepository notificationRepository)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _context = context;
            _channelRepository = channelRepository;
            _notificationRepository = notificationRepository;
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await _userManager.Users
                .Include(u => u.Channel)
                .Include(u => u.Application)
                .ToListAsync();
        }

        public async Task<User> GetUserById(string userId)
        {
            return await _userManager.Users
                .Include(u => u.Application)
                .FirstOrDefaultAsync(u => u.Id == userId);
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

        public async Task ChangeRoles(string id, string roleName)
        {
            var user = await _userManager.Users
                .Include(u => u.Channel)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (user != null)
            {
                if (user.Email == "nikitanik10305@gmail.com" || user.Email == "rupcyes@mail.com")
                {
                    return;
                }

                var userRoles = await _userManager.GetRolesAsync(user);

                if (userRoles.Contains(roleName))
                {
                    return;
                }

                await _userManager.RemoveFromRolesAsync(user, userRoles.ToArray());

                await _userManager.AddToRoleAsync(user, roleName);

                await SendNotification(user, roleName);
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

        private async Task ManageRoles(User user)
        {
            if (!_roleManager.RoleExistsAsync("Admin").GetAwaiter().GetResult())
            {
                await _roleManager.CreateAsync(new IdentityRole("Admin"));
                await _roleManager.CreateAsync(new IdentityRole("User"));
                await _roleManager.CreateAsync(new IdentityRole("Author"));
            }

            var role = (user.Email == "nikitanik10305@gmail.com" || user.Email == "rupcyes@mail.com") ? "Admin" : "User";
            await _userManager.AddToRoleAsync(user, role);
        }

        private async Task SendNotification(User user, string roleName)
        {
            if (roleName.Equals("Author"))
            {
                var notification = new Notification
                {
                    Message = "Вам стали доступны функции загрузки и удаления видео на странице вашего канала",
                    ChannelId = user.Channel.ChannelId
                };

                await _notificationRepository.AddNotification(notification);
            }
            else if (roleName.Equals("User"))
            {
                var notification = new Notification
                {
                    Message = "Вам не доступны функции загрузки и удаления видео",
                    ChannelId = user.Channel.ChannelId
                };

                await _notificationRepository.AddNotification(notification);
            }
        }

        private async Task DeleteChannel(string id)
        {
            var channel = await _context.Channels.FirstOrDefaultAsync(c => c.UserId == id);
            if (channel != null)
            {
                await _channelRepository.DeleteChannel(channel);
            }
        }
    }
}
