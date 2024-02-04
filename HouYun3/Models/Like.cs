using System.ComponentModel.DataAnnotations;

namespace HouYun3.Models
{
    public class Like
    {
        [Key]
        public int LikeId { get; set; }

        public int VideoId { get; set; }
        public Video Video { get; set; }

        public User User { get; set; }
    }
}
