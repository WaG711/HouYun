using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HouYun.Models
{
    public class Like
    {
        [Key]
        public Guid LikeId { get; set; }

        [ScaffoldColumn(false)]
        public DateTime LikeDate { get; set; }

        public Guid VideoId { get; set; }
        [ForeignKey("VideoId")]
        public Video Video { get; set; }

        public Guid ChannelId { get; set; }
        [ForeignKey("ChannelId")]
        public Channel Channel { get; set; }

        public Like()
        {
            LikeDate = DateTime.UtcNow;
        }
    }
}
