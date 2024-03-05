using System.ComponentModel.DataAnnotations;

namespace HouYun.ViewModels.forUser
{
    public class ChangeUserNameViewModel
    {
        [Required(ErrorMessage = "Поле Никнейм обязательно для заполнения")]
        [StringLength(15, MinimumLength = 4, ErrorMessage = "Длина поля Никнейм должна быть от 4 до 15 символов")]
        public string NewUserName { get; set; }
        [Required(ErrorMessage = "Поле Пароль обязательно для заполнения")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
