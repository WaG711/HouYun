﻿using System.ComponentModel.DataAnnotations;

namespace HouYun.ViewModels.forUser
{
    public class RegistrationViewModel
    {
        [Required(ErrorMessage = "Почта обязательна для заполнения")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Логин обязателен для заполнения")]
        [StringLength(15, MinimumLength = 4, ErrorMessage = "Длина Никнейм должна быть от 4 до 15 символов")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Пароль обязателен для заполнения")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}