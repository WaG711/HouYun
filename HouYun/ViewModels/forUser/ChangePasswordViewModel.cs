using System.ComponentModel.DataAnnotations;

namespace HouYun.ViewModels.forUser
{
    public class ChangePasswordViewModel
    {
        [Required(ErrorMessage = "Поле Новый пароль обязательно для заполнения.")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }
        [Required(ErrorMessage = "Поле Старый пароль обязательно для заполнения.")]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }
    }
}
