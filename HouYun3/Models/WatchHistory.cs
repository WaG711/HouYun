using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HouYun3.Models
{
    public class WatchHistory
    {
        [Key]
        public Guid WatchHistoryId { get; set; }

        [ScaffoldColumn(false)]
        [Display(Name = "Дата просмотра")]
        public DateTime WatchDate { get; set; }

        public Guid VideoId { get; set; }
        [ForeignKey("VideoId")]
        public Video Video { get; set; }

        public Guid ChannelId { get; set; }
        [ForeignKey("ChannelId")]
        public Channel Channel { get; set; }


        public WatchHistory()
        {
            WatchDate = DateTime.UtcNow;
        }
    }
}
