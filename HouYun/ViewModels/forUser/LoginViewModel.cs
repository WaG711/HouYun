using System.ComponentModel.DataAnnotations;

namespace HouYun.ViewModels.forUser
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Логин")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Введите пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
