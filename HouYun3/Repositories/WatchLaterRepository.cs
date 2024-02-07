﻿using HouYun3.IRepositories;
using HouYun3.Models;
using HouYun3.Data;
using Microsoft.EntityFrameworkCore;

namespace HouYun3.Repositories
{
    public class WatchLaterRepository : IWatchLaterRepository
    {
        private readonly ApplicationDbContext _context;

        public WatchLaterRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<WatchLater>> GetAllWatchLaterItems()
        {
            return await _context.WatchLaterItems.ToListAsync();
        }

        public async Task<WatchLater> GetWatchLaterItemById(Guid id)
        {
            return await _context.WatchLaterItems.FindAsync(id);
        }

        public async Task AddWatchLaterItem(WatchLater watchLaterItem)
        {
            _context.WatchLaterItems.Add(watchLaterItem);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteWatchLaterItem(Guid id)
        {
            var watchLaterItem = await _context.WatchLaterItems.FindAsync(id);
            if (watchLaterItem != null)
            {
                _context.WatchLaterItems.Remove(watchLaterItem);
                await _context.SaveChangesAsync();
            }
        }
    }
}
