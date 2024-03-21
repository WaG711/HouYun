using System.ComponentModel.DataAnnotations;

namespace HouYun.ViewModels.forUser
{
    public class UpdateChannelViewModel
    {
        [StringLength(50, MinimumLength = 4, ErrorMessage = "Имя канала должно быть от 4 до 50 символов")]
        [Display(Name = "Имя канала")]
        public string? ChannelName { get; set; }

        [StringLength(500, MinimumLength = 4, ErrorMessage = "Описание должно быть от 4 до 500 символов")]
        [Display(Name = "Описание")]
        public string? Description { get; set; }
    }
}
