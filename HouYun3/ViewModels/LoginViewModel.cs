using System.ComponentModel.DataAnnotations;

namespace HouYun3.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Введете Логин")]
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
