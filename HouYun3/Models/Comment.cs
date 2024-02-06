using System.ComponentModel.DataAnnotations;

namespace HouYun3.Models
{
    public class Comment
    {
        [Key]
        public int CommentId { get; set; }

        [Required(ErrorMessage = "Поле 'Текст комментария' обязательно для заполнения")]
        [StringLength(500, ErrorMessage = "Длина 'Текста комментария' не должна превышать 500 символов")]
        public string Text { get; set; }

        [ScaffoldColumn(false)]
        [Display(Name = "Дата комментария")]
        public DateTime CommentDate { get; set; }

        public int VideoId { get; set; }
        public Video Video { get; set; }

        [Display(Name = "Пользователь")]
        public string UserId { get; set; }
        public User User { get; set; }

        public Comment()
        {
            CommentDate = DateTime.UtcNow;
        }
    }
}
