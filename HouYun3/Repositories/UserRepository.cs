using HouYun3.IRepositories;
using HouYun3.Models;
using HouYun3.Data;
using Microsoft.EntityFrameworkCore;

namespace HouYun3.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly HouYun3Context _context;

        public UserRepository(HouYun3Context context)
        {
            _context = context;
        }

        public async Task<List<User>> GetAllUsers()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> GetUser(int userId)
        {
            return await _context.Users
                .Include(u => u.Videos)
                .Include(u => u.SearchHistory)
                .Include(u => u.WatchHistory)
                .Include(u => u.WatchLaterList)
                .Include(u => u.Notifications)
                .FirstOrDefaultAsync(u => u.UserID == userId);
        }

        public async Task AddUser(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateUser(User user)
        {
            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteUser(int userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }
    }
}
