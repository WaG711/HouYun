using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HouYun3.Models
{
    public class Notification
    {
        [Key]
        public Guid NotificationId { get; set; }

        [Required(ErrorMessage = "Поле 'Сообщение' обязательно для заполнения")]
        [StringLength(100, ErrorMessage = "Длина 'Сообщения' не должна превышать 100 символов")]
        public string Message { get; set; }

        [ScaffoldColumn(false)]
        [Display(Name = "Дата уведомления")]
        public DateTime NotificationDate { get; set; }

        [Display(Name = "Прочитано")]
        public bool IsRead { get; set; }

        public Guid ChannelId { get; set; }
        [ForeignKey("ChannelId")]
        public Channel Channel { get; set; }

        public Guid VideoId { get; set; }
        [ForeignKey("VideoId")]
        public Video Video { get; set; }

        public Notification()
        {
            NotificationDate = DateTime.UtcNow;
        }
    }
}
