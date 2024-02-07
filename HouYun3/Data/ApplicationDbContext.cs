﻿using HouYun3.Models;
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
                .HasKey(l => l.LikeId);

            modelBuilder.Entity<Like>()
                .HasOne(l => l.Video)
                .WithMany(v => v.Likes)
                .HasForeignKey(l => l.VideoId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Like>()
                .HasOne(l => l.User)
                .WithMany(u => u.Likes)
                .HasForeignKey(l => l.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Comment>()
                .HasKey(c => c.CommentId);

            modelBuilder.Entity<Comment>()
                .HasOne(c => c.Video)
                .WithMany(v => v.Comments)
                .HasForeignKey(c => c.VideoId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Comment>()
                .HasOne(c => c.User)
                .WithMany(u => u.Comments)
                .HasForeignKey(c => c.UserId)
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
                .WithMany(u => u.Views)
                .HasForeignKey(v => v.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<WatchLater>()
                .HasKey(w => w.WatchLaterId);

            modelBuilder.Entity<WatchLater>()
                .HasOne(w => w.Video)
                .WithMany(v => v.WatchLaterList)
                .HasForeignKey(w => w.VideoId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<WatchLater>()
                .HasOne(w => w.User)
                .WithMany(u => u.WatchLaterList)
                .HasForeignKey(w => w.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<WatchHistory>()
                .HasKey(w => w.WatchHistoryId);

            modelBuilder.Entity<WatchHistory>()
                .HasOne(w => w.Video)
                .WithMany(v => v.WatchHistories)
                .HasForeignKey(w => w.VideoId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<WatchHistory>()
                .HasOne(w => w.User)
                .WithMany(u => u.WatchHistory)
                .HasForeignKey(w => w.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<SearchHistory>()
                .HasKey(s => s.SearchHistoryId);

            modelBuilder.Entity<SearchHistory>()
                .HasOne(s => s.User)
                .WithMany(u => u.SearchHistory)
                .HasForeignKey(s => s.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Notification>()
                .HasKey(n => n.NotificationId);

            modelBuilder.Entity<Notification>()
                .HasOne(n => n.User)
                .WithMany(u => u.Notifications)
                .HasForeignKey(n => n.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
