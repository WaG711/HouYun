using System.ComponentModel.DataAnnotations;

namespace HouYun.ViewModels.forUser
{
    public class RegistrationViewModel
    {
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Поле Логин обязательно для заполнения.")]
        [StringLength(15, MinimumLength = 4, ErrorMessage = "Длина поля Никнейм должна быть от 4 до 15 символов")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Поле Пароль обязательно для заполнения.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}