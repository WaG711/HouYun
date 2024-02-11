using HouYun3.Models;
using System.ComponentModel.DataAnnotations;

namespace HouYun3.ViewModels.forVideo
{
    public class AddVideoViewModel
    {
        [Required(ErrorMessage = "Поле 'Название видео' обязательно для заполнения")]
        [Display(Name = "Название видео")]
        [StringLength(100, ErrorMessage = "Длина 'Названия' не должна превышать 100 символов")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Поле 'Описание видео' обязательно для заполнения")]
        [Display(Name = "Описание видео")]
        [StringLength(500, ErrorMessage = "Длина 'Описания' не должна превышать 500 символов")]
        public string Description { get; set; }

        [Display(Name = "Категория видео")]
        [Required(ErrorMessage = "Поле 'Категория видео' обязательно для заполнения")]
        public Guid CategoryId { get; set; }

        [Display(Name = "Файл видео")]
        [Required(ErrorMessage = "Выберите видеофайл")]
        public IFormFile VideoFile { get; set; }

        [Display(Name = "Файл постера")]
        [Required(ErrorMessage = "Выберите постер")]
        public IFormFile PosterFile { get; set; }

        public IEnumerable<Category>? Categories { get; set; }
    }
}
