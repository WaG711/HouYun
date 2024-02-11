﻿using HouYun3.Data;
using HouYun3.IRepositories;
using HouYun3.Models;
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
                .FirstOrDefaultAsync(c => c.UserId == userId);
        }

        public async Task<Channel> GetChannelById(Guid id)
        {
            return await _context.Channels.FindAsync(id);
        }

        public async Task<IEnumerable<Channel>> GetAllChannels()
        {
            return await _context.Channels.ToListAsync();
        }

        public async Task CreateChannel(Channel channel)
        {
            await _context.Channels.AddAsync(channel);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateChannel(Channel channel)
        {
            _context.Channels.Update(channel);
            await _context.SaveChangesAsync();
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