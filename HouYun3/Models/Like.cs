using System.ComponentModel.DataAnnotations;
using HouYun3.ApplicationModel;

namespace HouYun3.Models
{
    public class Like
    {
        [Key]
        public int LikeId { get; set; }

        public int VideoId { get; set; }
        public Video Video { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }
    }
}
