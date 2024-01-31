using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using HouYun2.Models;

namespace HouYun3.Data
{
    public class HouYun3Context : DbContext
    {
        public HouYun3Context (DbContextOptions<HouYun3Context> options)
            : base(options)
        {
        }

        public DbSet<HouYun2.Models.User> Users { get; set; } = default!;
        public DbSet<HouYun2.Models.Video> Videos { get; set; } = default!;
        public DbSet<HouYun2.Models.Category> Categories { get; set; } = default!;
        public DbSet<HouYun2.Models.Comment> Comments { get; set; } = default!;
        public DbSet<HouYun2.Models.Like> Likes { get; set; } = default!;
        public DbSet<HouYun2.Models.Notification> Notifications { get; set; } = default!;
        public DbSet<HouYun2.Models.SearchHistory> SearchHistories { get; set; } = default!;
        public DbSet<HouYun2.Models.View> Views { get; set; } = default!;
        public DbSet<HouYun2.Models.WatchHistory> WatchHistories { get; set; } = default!;
        public DbSet<HouYun2.Models.WatchLater> WatchLaters { get; set; } = default!;
    }
}
