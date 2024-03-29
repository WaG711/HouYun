﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HouYun.Models
{
    public class Channel
    {
        public Guid ChannelId { get; set; }

        public string Name { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        [ScaffoldColumn(false)]
        public DateTime RegistrationDate  { get; set; }

        public string BannerPath { get; set; }

        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }

        public ICollection<Subscription> Subscribers { get; set; }
        public ICollection<Video> Videos { get; set; }
        public ICollection<SearchHistory> SearchHistories { get; set; }
        public ICollection<WatchHistory> WatchHistories { get; set; }
        public ICollection<WatchLater> WatchLaterList { get; set; }
        public ICollection<Notification> Notifications { get; set; }
        public ICollection<View> Views { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<Like> Likes { get; set; }

        public Channel()
        {
            Subscribers = new List<Subscription>();
            SearchHistories = new List<SearchHistory>();
            WatchHistories = new List<WatchHistory>();
            WatchLaterList = new List<WatchLater>();
            Notifications = new List<Notification>();
            Views = new List<View>();
            Comments = new List<Comment>();
            Likes = new List<Like>();
            Videos = new List<Video>();
            BannerPath = "banner.png";
            RegistrationDate = DateTime.UtcNow;
        }
    }
}
