﻿using System.ComponentModel.DataAnnotations;
using HouYun3.ApplicationModel;

namespace HouYun3.Models
{
    public class Notification
    {
        [Key]
        public int NotificationId { get; set; }

        [Required(ErrorMessage = "Поле 'Сообщение' обязательно для заполнения")]
        [StringLength(100, ErrorMessage = "Длина 'Сообщения' не должна превышать 100 символов")]
        public string Message { get; set; }

        [ScaffoldColumn(false)]
        [Display(Name = "Дата уведомления")]
        public DateTime NotificationDate { get; set; }

        [Display(Name = "Прочитано")]
        public bool IsRead { get; set; }

        [Display(Name = "Пользователь")]
        public string UserId { get; set; }
        public User User { get; set; }

        public Notification()
        {
            NotificationDate = DateTime.UtcNow;
        }
    }
}
