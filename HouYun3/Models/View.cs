using HouYun3.ApplicationModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HouYun3.Models
{
    public class View
    {
        [Key]
        public int ViewId { get; set; }

        public int VideoId { get; set; }
        [ForeignKey("VideoId")]
        public Video Video { get; set; }

        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }
    }
}
