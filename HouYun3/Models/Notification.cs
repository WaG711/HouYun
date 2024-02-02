using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HouYun3.Models
{
    public class Notification
    {
        [Key]
        public int NotificationID { get; set; }

        [Required(ErrorMessage = "Поле 'Сообщение' обязательно для заполнения")]
        [StringLength(100, ErrorMessage = "Длина 'Сообщения' не должна превышать 100 символов")]
        public string Message { get; set; }

        [ScaffoldColumn(false)]
        [Display(Name = "Дата уведомления")]
        public DateTime NotificationDate { get; set; }

        [Display(Name = "Прочитано")]
        public bool IsRead { get; set; }

        [Required(ErrorMessage = "Поле 'Пользователь ID' обязательно для заполнения")]
        public int UserID { get; set; }

        [ForeignKey("UserID")]
        public User User { get; set; }


        public Notification()
        {
            NotificationDate = DateTime.Now;
        }
    }
}
