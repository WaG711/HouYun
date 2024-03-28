using System.ComponentModel.DataAnnotations;

namespace HouYun.ViewModels.forUser
{
    public class UpdateBannerChannelViewModel
    {
        [Display(Name = "Файл баннера")]
        [Required(ErrorMessage = "Выберите файл")]
        public IFormFile BannerFile { get; set; }
    }
}
