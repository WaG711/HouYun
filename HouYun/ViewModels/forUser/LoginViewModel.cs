using System.ComponentModel.DataAnnotations;

namespace HouYun.ViewModels.forUser
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Логин")]
        [Display(Name = "Логин")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Введите пароль")]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [Display(Name = "Запомнить?")]
        public bool RememberMe { get; set; }
    }
}
