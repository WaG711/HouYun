using HouYun3.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HouYun3.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Video> Videos { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<View> Views { get; set; }
        public DbSet<WatchLater> WatchLaterItems { get; set; }
        public DbSet<WatchHistory> WatchHistories { get; set; }
        public DbSet<SearchHistory> SearchHistories { get; set; }
        public DbSet<Notification> Notifications { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Like>()
                .HasKey(v => v.LikeId);

            modelBuilder.Entity<Like>()
                .HasOne(v => v.Video)
                .WithMany(v => v.Likes)
                .HasForeignKey(v => v.VideoId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Like>()
                .HasOne(v => v.User)
                .WithMany(v => v.Likes)
                .HasForeignKey(v => v.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Comment>()
                .HasKey(v => v.CommentId);

            modelBuilder.Entity<Comment>()
                .HasOne(v => v.Video)
                .WithMany(v => v.Comments)
                .HasForeignKey(v => v.VideoId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Comment>()
                .HasOne(v => v.User)
                .WithMany(v => v.Comments)
                .HasForeignKey(v => v.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<View>()
                .HasKey(v => v.ViewId);

            modelBuilder.Entity<View>()
                .HasOne(v => v.Video)
                .WithMany(v => v.Views)
                .HasForeignKey(v => v.VideoId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<View>()
                .HasOne(v => v.User)
                .WithMany(v => v.Views)
                .HasForeignKey(v => v.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<WatchLater>()
                .HasKey(v => v.WatchLaterId);

            modelBuilder.Entity<WatchLater>()
                .HasOne(v => v.Video)
                .WithMany(v => v.WatchLaterList)
                .HasForeignKey(v => v.VideoId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<WatchLater>()
                .HasOne(v => v.User)
                .WithMany(v => v.WatchLaterList)
                .HasForeignKey(v => v.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<WatchHistory>()
                .HasKey(v => v.WatchHistoryId);

            modelBuilder.Entity<WatchHistory>()
                .HasOne(v => v.Video)
                .WithMany(v => v.WatchHistories)
                .HasForeignKey(v => v.VideoId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<WatchHistory>()
                .HasOne(v => v.User)
                .WithMany(v => v.WatchHistory)
                .HasForeignKey(v => v.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<SearchHistory>()
                .HasKey(v => v.SearchHistoryId);

            modelBuilder.Entity<SearchHistory>()
                .HasOne(v => v.User)
                .WithMany(v => v.SearchHistory)
                .HasForeignKey(v => v.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Notification>()
                .HasKey(v => v.NotificationId);

            modelBuilder.Entity<Notification>()
                .HasOne(v => v.User)
                .WithMany(v => v.Notifications)
                .HasForeignKey(v => v.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
