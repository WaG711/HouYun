using HouYun.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HouYun.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Video> Videos { get; set; }
        public DbSet<Channel> Channels { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
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

            modelBuilder.Entity<Subscription>()
                .HasKey(s => s.SubscriptionId);

            modelBuilder.Entity<Subscription>()
                .HasOne(s => s.User)
                .WithMany(u => u.Subscriptions)
                .HasForeignKey(s => s.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Subscription>()
                .HasOne(s => s.Channel)
                .WithMany(c => c.Subscribers)
                .HasForeignKey(s => s.ChannelId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Channel>()
                .HasKey(c => c.ChannelId);

            modelBuilder.Entity<Channel>()
                .HasIndex(c => c.Name)
                .IsUnique();

            modelBuilder.Entity<Channel>()
                .HasOne(c => c.User)
                .WithOne(u => u.Channel)
                .HasForeignKey<Channel>(c => c.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Like>()
                .HasKey(l => l.LikeId);

            modelBuilder.Entity<Like>()
                .HasOne(l => l.Video)
                .WithMany(v => v.Likes)
                .HasForeignKey(l => l.VideoId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Like>()
                .HasOne(l => l.Channel)
                .WithMany(u => u.Likes)
                .HasForeignKey(l => l.ChannelId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Comment>()
                .HasKey(c => c.CommentId);

            modelBuilder.Entity<Comment>()
                .HasOne(c => c.Video)
                .WithMany(v => v.Comments)
                .HasForeignKey(c => c.VideoId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Comment>()
                .HasOne(c => c.Channel)
                .WithMany(u => u.Comments)
                .HasForeignKey(c => c.ChannelId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<View>()
                .HasKey(v => v.ViewId);

            modelBuilder.Entity<View>()
                .HasOne(v => v.Video)
                .WithMany(v => v.Views)
                .HasForeignKey(v => v.VideoId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<View>()
                .HasOne(v => v.Channel)
                .WithMany(u => u.Views)
                .HasForeignKey(v => v.ChannelId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<WatchLater>()
                .HasKey(w => w.WatchLaterId);

            modelBuilder.Entity<WatchLater>()
                .HasOne(w => w.Video)
                .WithMany(w => w.WatchLaterItems)
                .HasForeignKey(w => w.VideoId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<WatchLater>()
                .HasOne(w => w.Channel)
                .WithMany(u => u.WatchLaterList)
                .HasForeignKey(w => w.ChannelId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<WatchHistory>()
                .HasKey(w => w.WatchHistoryId);

            modelBuilder.Entity<WatchHistory>()
                .HasOne(w => w.Video).
                WithMany(v => v.WatchHistories)
                .HasForeignKey(w => w.VideoId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<WatchHistory>()
                .HasOne(w => w.Channel)
                .WithMany(u => u.WatchHistories)
                .HasForeignKey(w => w.ChannelId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<SearchHistory>()
                .HasKey(s => s.SearchHistoryId);

            modelBuilder.Entity<SearchHistory>()
                .HasOne(s => s.Channel)
                .WithMany(u => u.SearchHistories)
                .HasForeignKey(s => s.ChannelId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Notification>()
                .HasKey(n => n.NotificationId);

            modelBuilder.Entity<Notification>()
                .HasOne(n => n.Video)
                .WithMany(v => v.Notifications)
                .HasForeignKey(n => n.VideoId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Notification>()
                .HasOne(n => n.Channel)
                .WithMany(u => u.Notifications)
                .HasForeignKey(n => n.ChannelId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
