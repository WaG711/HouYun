using System.ComponentModel.DataAnnotations;

namespace HouYun.ViewModels.forUser
{
    public class ChangePasswordViewModel
    {
        [Required(ErrorMessage = "Новый пароль обязательно для заполнения")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }
        [Required(ErrorMessage = "Старый пароль обязательно для заполнения")]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }
    }
}
