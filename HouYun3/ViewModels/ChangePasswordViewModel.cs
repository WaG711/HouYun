using System.ComponentModel.DataAnnotations;

namespace HouYun3.ViewModels
{
    public class ChangePasswordViewModel
    {
        [Required(ErrorMessage = "Новый пароль")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }
        [Required(ErrorMessage = "Старый пароль")]
        [DataType(DataType.Password)]
        public string OldPassword { get; set;}
    }
}
