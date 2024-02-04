using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HouYun3.Models
{
    public class Video
    {
        [Key]
        public int VideoId { get; set; }

        [Required(ErrorMessage = "Поле 'Название видео' обязательно для заполнения")]
        [Display(Name = "Название видео")]
        [StringLength(100, ErrorMessage = "Длина 'Названия' не должна превышать 100 символов")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Поле 'Описание видео' обязательно для заполнения")]
        [Display(Name = "Описание видео")]
        [StringLength(500, ErrorMessage = "Длина 'Описания' не должна превышать 500 символов")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Поле 'Продолжительность видео' обязательно для заполнения")]
        [Display(Name = "Продолжительность видео")]
        [Range(0, int.MaxValue, ErrorMessage = "Значение 'Продолжительность' должно быть неотрицательным")]
        public int DurationSeconds { get; set; }

        [BindNever]
        public string FilePath { get; set; }

        [NotMapped]
        [Display(Name = "Файл видео")]
        [Required(ErrorMessage = "Выберите видеофайл")]
        public IFormFile VideoFile { get; set; }

        [ScaffoldColumn(false)]
        public DateTime UploadDate { get; set; }

        public ICollection<Like> Likes { get; set; } = new List<Like>();
        public ICollection<View> Views { get; set; } = new List<View>();

        [Display(Name = "Категория видео")]
        [Required(ErrorMessage = "Поле 'Категория видео' обязательно для заполнения")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        [Display(Name = "Пользователь")]
        public User User { get; set; }

        public ICollection<Comment> Comments { get; set; } = new List<Comment>();


        public Video()
        {
            UploadDate = DateTime.Now;
        }
    }
}
