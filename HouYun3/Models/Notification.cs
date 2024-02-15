using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HouYun.Models
{
    public class Notification
    {
        [Key]
        public Guid NotificationId { get; set; }

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
