﻿using System.ComponentModel.DataAnnotations;
using HouYun3.ApplicationModel;

namespace HouYun3.Models
{
    public class WatchLater
    {
        [Key]
        public int WatchLaterId { get; set; }

        [ScaffoldColumn(false)]
        [Display(Name = "Дата добавления в список 'Посмотреть позже'")]
        public DateTime WatchDate { get; set; }

        public int VideoId { get; set; }
        public Video Video { get; set; }

        [Display(Name = "Пользователь")]
        public string UserId { get; set; }
        public User User { get; set; }


        public WatchLater()
        {
            WatchDate = DateTime.UtcNow;
        }
    }
}
