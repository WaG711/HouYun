using System.ComponentModel.DataAnnotations;

namespace HouYun.ViewModels.forUser
{
    public class ChangePasswordViewModel
    {
        [Required]
        [Display(Name = "Новый Пароль")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }
        [Required]
        [Display(Name = "Старый Пароль")]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }
    }
}
