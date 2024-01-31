using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HouYun2.Models
{
    public class View
    {
        [Key]
        public int ViewID { get; set; }

        [Required(ErrorMessage = "Поле 'Видео ID' обязательно для заполнения")]
        public int VideoID { get; set; }

        [ForeignKey("VideoID")]
        public Video Video { get; set; }

        [Required(ErrorMessage = "Поле 'Пользователь ID' обязательно для заполнения")]
        public int UserID { get; set; }

        [ForeignKey("UserID")]
        public User User { get; set; }
    }
}
