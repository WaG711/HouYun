using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HouYun.Models
{
    public class Video
    {
        [Key]
        public Guid VideoId { get; set; }

        [StringLength(150)]
        public string Title { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        [ScaffoldColumn(false)]
        public string VideoPath { get; set; }

        [ScaffoldColumn(false)]
        public string PosterPath { get; set; }

        [NotMapped]
        public IFormFile VideoFile { get; set; }

        [NotMapped]
        public IFormFile PosterFile { get; set; }

        [ScaffoldColumn(false)]
        public DateTime UploadDate { get; set; }

        public Guid CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public Category Category { get; set; }

        public Guid ChannelId { get; set; }
        [ForeignKey("ChannelId")]
        public Channel Channel { get; set; }

        public ICollection<Notification> Notifications { get; set; }
        public ICollection<WatchHistory> WatchHistories { get; set; }
        public ICollection<WatchLater> WatchLaterItems { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<Like> Likes { get; set; }
        public ICollection<View> Views { get; set; }


        public Video()
        {
            UploadDate = DateTime.UtcNow;
            Comments = new List<Comment>();
            Likes = new List<Like>();
            Views = new List<View>();
            WatchHistories = new List<WatchHistory>();
            WatchLaterItems = new List<WatchLater>();
            Notifications = new List<Notification>();
        }
    }
}
