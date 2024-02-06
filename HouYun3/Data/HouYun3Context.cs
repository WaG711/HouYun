using HouYun3.Models;
using Microsoft.EntityFrameworkCore;


namespace HouYun3.Data
{
    public class HouYun3Context : DbContext
    {
        public HouYun3Context (DbContextOptions<HouYun3Context> options)
            : base(options)
        {
        }

        public DbSet<Video> Videos { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<SearchHistory> SearchHistories { get; set; }
        public DbSet<View> Views { get; set; }
        public DbSet<WatchHistory> WatchHistories { get; set; }
        public DbSet<WatchLater> WatchLaters { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Like>()
                .HasOne(v => v.User)
                .WithMany(v => v.Likes)
                .HasForeignKey(v => v.UserId);

            modelBuilder.Entity<Comment>()
                .HasOne(v => v.User)
                .WithMany(v => v.Comments)
                .HasForeignKey(v => v.UserId);

            modelBuilder.Entity<View>()
                .HasOne(v => v.User)
                .WithMany(v => v.Views)
                .HasForeignKey(v => v.UserId);

            modelBuilder.Entity<WatchLater>()
                .HasOne(v => v.User)
                .WithMany(v => v.WatchLaterList)
                .HasForeignKey(v => v.UserId);

            modelBuilder.Entity<WatchHistory>()
                .HasOne(v => v.User)
                .WithMany(v => v.WatchHistory)
                .HasForeignKey(v => v.UserId);

            modelBuilder.Entity<SearchHistory>()
                .HasOne(v => v.User)
                .WithMany(v => v.SearchHistory)
                .HasForeignKey(v => v.UserId);

            modelBuilder.Entity<Notification>()
                .HasOne(v => v.User)
                .WithMany(v => v.Notifications)
                .HasForeignKey(v => v.UserId);
        }
    }
}
