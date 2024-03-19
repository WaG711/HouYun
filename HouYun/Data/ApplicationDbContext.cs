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
                .WithMany(c => c.Likes)
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
                .HasOne(cm => cm.Channel)
                .WithMany(ch => ch.Comments)
                .HasForeignKey(cm => cm.ChannelId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<View>()
                .HasKey(v => v.ViewId);

            modelBuilder.Entity<View>()
                .HasOne(vw => vw.Video)
                .WithMany(vd => vd.Views)
                .HasForeignKey(vw => vw.VideoId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<View>()
                .HasOne(v => v.Channel)
                .WithMany(c => c.Views)
                .HasForeignKey(v => v.ChannelId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<WatchLater>()
                .HasKey(wl => wl.WatchLaterId);

            modelBuilder.Entity<WatchLater>()
                .HasOne(wl => wl.Video)
                .WithMany(v => v.WatchLaterItems)
                .HasForeignKey(wl => wl.VideoId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<WatchLater>()
                .HasOne(wl => wl.Channel)
                .WithMany(c => c.WatchLaterList)
                .HasForeignKey(wl => wl.ChannelId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<WatchHistory>()
                .HasKey(wh => wh.WatchHistoryId);

            modelBuilder.Entity<WatchHistory>()
                .HasOne(wh => wh.Video).
                WithMany(v => v.WatchHistories)
                .HasForeignKey(wh => wh.VideoId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<WatchHistory>()
                .HasOne(wh => wh.Channel)
                .WithMany(c => c.WatchHistories)
                .HasForeignKey(wh => wh.ChannelId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<SearchHistory>()
                .HasKey(s => s.SearchHistoryId);

            modelBuilder.Entity<SearchHistory>()
                .HasOne(s => s.Channel)
                .WithMany(c => c.SearchHistories)
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
                .WithMany(c => c.Notifications)
                .HasForeignKey(n => n.ChannelId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
