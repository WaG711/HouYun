﻿using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace HouYun3.Models
{
    public class User : IdentityUser
    {
        [Key]
        public override string Id { get; set; }

        [Required(ErrorMessage = "Поле 'Email' обязательно для заполнения")]
        [EmailAddress(ErrorMessage = "Некорректный формат 'Email'")]
        public override string Email { get; set; }

        [Required(ErrorMessage = "Поле 'Логин' обязательно для заполнения")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Длина 'Логина' должна быть от 3 до 50 символов")]
        public override string UserName { get; set; }

        public Channel Channel { get; set; }
        public ICollection<Video> Videos { get; set; }
        public ICollection<SearchHistory> SearchHistory { get; set; }
        public ICollection<WatchHistory> WatchHistory { get; set; }
        public ICollection<WatchLater> WatchLaterList { get; set; }
        public ICollection<Notification> Notifications { get; set; }
        public ICollection<View> Views { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<Like> Likes { get; set; }
        public ICollection<Subscription> Subscriptions { get; set; }

        public User()
        {
            Videos = new List<Video>();
            SearchHistory = new List<SearchHistory>();
            WatchHistory = new List<WatchHistory>();
            WatchLaterList = new List<WatchLater>();
            Notifications = new List<Notification>();
            Views = new List<View>();
            Comments = new List<Comment>();
            Likes = new List<Like>();
            Subscriptions = new List<Subscription>();
            Channel = new Channel { UserId = Id };
        }
    }
}
