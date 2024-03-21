using System.ComponentModel.DataAnnotations;

namespace HouYun.ViewModels.forUser
{
    public class ChangeUserNameViewModel
    {
        [Required(ErrorMessage = "Имя пользователя обязательно для заполнения")]
        [StringLength(50, MinimumLength = 4, ErrorMessage = "Имя пользователя должно быть от 4 до 50 символов")]
        public string NewUserName { get; set; }

        [Required(ErrorMessage = "Пароль обязателен для заполнения")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
