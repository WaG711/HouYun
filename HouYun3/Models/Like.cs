using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HouYun3.Models
{
    public class Like
    {
        [Key]
        public int LikeID { get; set; }

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
