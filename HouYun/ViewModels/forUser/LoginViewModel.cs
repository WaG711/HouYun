﻿using System.ComponentModel.DataAnnotations;

namespace HouYun.ViewModels.forUser
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Логин обязателен для заполнения")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Пароль обязателен для заполнения")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
