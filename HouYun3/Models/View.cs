using System.ComponentModel.DataAnnotations;

namespace HouYun3.Models
{
    public class View
    {
        [Key]
        public int ViewId { get; set; }

        public int VideoId { get; set; }
        public Video Video { get; set; }

        [Display(Name = "Пользователь")]
        public string UserId { get; set; }
        public User User { get; set; }
    }
}
