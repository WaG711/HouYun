using System.ComponentModel.DataAnnotations;

namespace HouYun3.Models
{
    public class Video
    {
        [Key]
        public int VideoID { get; set; }

        [Required(ErrorMessage = "Поле 'Название' обязательно для заполнения")]
        [StringLength(100, ErrorMessage = "Длина 'Названия' не должна превышать 100 символов")]
        public string Title { get; set; }

        [StringLength(500, ErrorMessage = "Длина 'Описания' не должна превышать 500 символов")]
        public string Description { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Значение 'Продолжительность' должно быть неотрицательным")]
        public int DurationSeconds { get; set; }

        [Required(ErrorMessage = "Поле 'Путь к файлу' обязательно для заполнения")]
        public string FilePath { get; set; }

        [ScaffoldColumn(false)]
        [Display(Name = "Дата загрузки")]
        public DateTime UploadDate { get; set; }

        public ICollection<Like> Likes { get; set; } = new List<Like>();

        public ICollection<View> Views { get; set; } = new List<View>();

        [Required(ErrorMessage = "Поле 'Категория' обязательно для заполнения")]
        public int CategoryID { get; set; }

        [Display(Name = "Категория")]
        public Category Category { get; set; }

        [Required(ErrorMessage = "Поле 'Пользователь' обязательно для заполнения")]
        public int UserID { get; set; }

        [Display(Name = "Пользователь")]
        public User User { get; set; }

        public ICollection<Comment> Comments { get; set; } = new List<Comment>();


        public Video()
        {
            UploadDate = DateTime.Now;
        }
    }
}
