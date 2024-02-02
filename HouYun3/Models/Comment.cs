using System.ComponentModel.DataAnnotations;

namespace HouYun3.Models
{
    public class Comment
    {
        [Key]
        public int CommentID { get; set; }

        [Required(ErrorMessage = "Поле 'Текст комментария' обязательно для заполнения")]
        [StringLength(500, ErrorMessage = "Длина 'Текста комментария' не должна превышать 500 символов")]
        public string Text { get; set; }

        [ScaffoldColumn(false)]
        [Display(Name = "Дата комментария")]
        public DateTime CommentDate { get; set; }

        [Required(ErrorMessage = "Поле 'Видео' обязательно для заполнения")]
        public int VideoID { get; set; }

        [Display(Name = "Видео")]
        public Video Video { get; set; }

        [Required(ErrorMessage = "Поле 'Пользователь' обязательно для заполнения")]
        public int UserID { get; set; }

        [Display(Name = "Пользователь")]
        public User User { get; set; }


        public Comment()
        {
            CommentDate = DateTime.Now;
        }
    }
}
