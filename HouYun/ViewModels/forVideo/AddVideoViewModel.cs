using HouYun.Models;
using System.ComponentModel.DataAnnotations;

namespace HouYun.ViewModels.forVideo
{
    public class AddVideoViewModel
    {
        [Required(ErrorMessage = "Название видео обязательно для заполнения")]
        [Display(Name = "Название видео")]
        [StringLength(100, ErrorMessage = "Название не должна превышать 100 символов")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Описание видео обязательно для заполнения")]
        [Display(Name = "Описание видео")]
        [StringLength(500, ErrorMessage = "Описание не должна превышать 500 символов")]
        public string Description { get; set; }

        [Display(Name = "Категория видео")]
        [Required(ErrorMessage = "Категория обязательна для выбора")]
        public Guid? CategoryId { get; set; }

        [Display(Name = "Файл видео")]
        [Required(ErrorMessage = "Выберите видеофайл")]
        public IFormFile VideoFile { get; set; }

        [Display(Name = "Файл постера")]
        [Required(ErrorMessage = "Выберите постер")]
        public IFormFile PosterFile { get; set; }

        public IEnumerable<Category>? Categories { get; set; }
    }
}
