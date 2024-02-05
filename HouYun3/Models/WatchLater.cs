using System.ComponentModel.DataAnnotations;

namespace HouYun3.Models
{
    public class WatchLater
    {
        [Key]
        public int WatchLaterId { get; set; }

        [ScaffoldColumn(false)]
        [Display(Name = "Дата добавления в список 'Посмотреть позже'")]
        public DateTime WatchDate { get; set; }

        public int VideoId { get; set; }
        public Video Video { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }


        public WatchLater()
        {
            WatchDate = DateTime.Now;
        }
    }
}
