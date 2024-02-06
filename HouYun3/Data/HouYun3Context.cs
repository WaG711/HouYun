using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using HouYun3.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace HouYun3.Data
{
    public class HouYun3Context : DbContext
    {
        public HouYun3Context (DbContextOptions<HouYun3Context> options)
            : base(options)
        {
        }

        public DbSet<HouYun3.Models.Video> Videos { get; set; } = default!;
        public DbSet<HouYun3.Models.Category> Categories { get; set; } = default!;
        public DbSet<HouYun3.Models.Comment> Comments { get; set; } = default!;
        public DbSet<HouYun3.Models.Like> Likes { get; set; } = default!;
        public DbSet<HouYun3.Models.Notification> Notifications { get; set; } = default!;
        public DbSet<HouYun3.Models.SearchHistory> SearchHistories { get; set; } = default!;
        public DbSet<HouYun3.Models.View> Views { get; set; } = default!;
        public DbSet<HouYun3.Models.WatchHistory> WatchHistories { get; set; } = default!;
        public DbSet<HouYun3.Models.WatchLater> WatchLaters { get; set; } = default!;
    }
}
