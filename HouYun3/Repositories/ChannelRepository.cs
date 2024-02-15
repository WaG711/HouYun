using HouYun3.Data;
using HouYun3.IRepositories;
using HouYun3.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace HouYun3.Repositories
{
    public class ChannelRepository : IChannelRepository
    {
        private readonly ApplicationDbContext _context;

        public ChannelRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Channel> GetChannelByName(string channelName)
        {
            return await _context.Channels
                .Include(v => v.Subscribers)
                .Include(v => v.Videos)
                    .ThenInclude(v => v.Views)
                .FirstOrDefaultAsync(c => c.Name == channelName);
        }

        public async Task<Guid> GetChannelIdByUserId(string userId)
        {
            var channel = await _context.Channels.FirstOrDefaultAsync(c => c.UserId == userId);
            return channel.ChannelId;
        }

        public async Task<Channel> GetChannelByUserId(string userId)
        {
            return await _context.Channels
                .Include(v => v.Videos)
                    .ThenInclude(v => v.Views)
                .Include(v => v.Subscribers)
                .FirstOrDefaultAsync(c => c.UserId == userId);
        }

        public async Task<IEnumerable<Channel>> GetAllChannels()
        {
            return await _context.Channels.ToListAsync();
        }

        public async Task UpdateChannel(Channel channel)
        {
            try
            {
                _context.Entry(channel).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                if (ex.InnerException is SqlException sqlException && (sqlException.Number == 2601 || sqlException.Number == 2627))
                {
                    throw new Exception("Имя канала уже используется");
                }
            }
        }


        public async Task DeleteChannel(Guid id)
        {
            var channel = await _context.Channels.FindAsync(id);

            if (channel != null)
            {
                _context.Channels.Remove(channel);
                await _context.SaveChangesAsync();
            }
        }
    }
}
