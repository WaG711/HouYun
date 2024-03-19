using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HouYun.Models
{
    public class Comment
    {
        [Key]
        public Guid CommentId { get; set; }

        [StringLength(500)]
        public string Text { get; set; }

        [ScaffoldColumn(false)]
        public DateTime CommentDate { get; set; }

        public Guid VideoId { get; set; }
        [ForeignKey("VideoId")]
        public Video Video { get; set; }

        public Guid ChannelId { get; set; }
        [ForeignKey("ChannelId")]
        public Channel Channel { get; set; }

        public Comment()
        {
            CommentDate = DateTime.UtcNow;
        }
    }
}
