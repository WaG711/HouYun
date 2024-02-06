using HouYun3.ApplicationModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        [ForeignKey("VideoId")]
        public Video Video { get; set; }

        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }

        public Comment()
        {
            CommentDate = DateTime.UtcNow;
        }
    }
}
