using System.ComponentModel.DataAnnotations;
using HouYun3.ApplicationModel;

namespace HouYun3.Models
{
    public class WatchHistory
    {
        [Key]
        public int WatchHistoryId { get; set; }

        [ScaffoldColumn(false)]
        [Display(Name = "Дата просмотра")]
        public DateTime WatchDate { get; set; }

        public int VideoId { get; set; }
        public Video Video { get; set; }

        [Display(Name = "Пользователь")]
        public string UserId { get; set; }
        public User User { get; set; }


        public WatchHistory()
        {
            WatchDate = DateTime.UtcNow;
        }
    }
}
