﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HouYun.Models
{
    public class WatchLater
    {
        [Key]
        public Guid WatchLaterId { get; set; }

        [ScaffoldColumn(false)]
        public DateTime WatchLaterDate { get; set; }

        public Guid VideoId { get; set; }
        [ForeignKey("VideoId")]
        public Video Video { get; set; }

        public Guid ChannelId { get; set; }
        [ForeignKey("ChannelId")]
        public Channel Channel { get; set; }

        public WatchLater()
        {
            WatchLaterDate = DateTime.UtcNow;
        }
    }
}
