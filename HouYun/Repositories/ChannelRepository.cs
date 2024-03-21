using HouYun.Data;
using HouYun.IRepositories;
using HouYun.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace HouYun.Repositories
{
    public class ChannelRepository : IChannelRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IVideoRepository _videoRepository;

        public ChannelRepository(ApplicationDbContext context, IVideoRepository videoRepository)
        {
            _context = context;
            _videoRepository = videoRepository;
        }

        public async Task<Guid> GetChannelIdByUserId(string userId)
        {
            var channel = await _context.Channels.FirstOrDefaultAsync(c => c.UserId == userId);
            return channel.ChannelId;
        }

        public async Task<Channel> GetChannelByName(string channelName)
        {
            return await GetChannelInfo(c => c.Name == channelName);
        }

        public async Task<Channel> GetChannelByUserId(string userId)
        {
            return await GetChannelInfo(c => c.UserId == userId);
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

        public async Task DeleteChannel(Channel channel)
        {
            var videos = await _context.Videos
                .Where(v => v.ChannelId == channel.ChannelId)
                .ToArrayAsync();

            foreach (var video in videos)
            {
                await _videoRepository.DeleteVideo(video.VideoId);
            }

            _context.Channels.Remove(channel);
            await _context.SaveChangesAsync();
        }

        private async Task<Channel> GetChannelInfo(Expression<Func<Channel, bool>> expression)
        {
            return await _context.Channels
                .Include(c => c.Videos.OrderByDescending(v => v.UploadDate))
                    .ThenInclude(v => v.Views)
                .Include(c => c.Subscribers)
                .FirstOrDefaultAsync(expression);
        }
    }
}
