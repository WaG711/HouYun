using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HouYun.Models
{
    public class SearchHistory
    {
        [Key]
        public Guid SearchHistoryId { get; set; }

        [StringLength(100)]
        public string SearchQuery { get; set; }

        [ScaffoldColumn(false)]
        public DateTime SearchDate { get; set; }

        public Guid ChannelId { get; set; }
        [ForeignKey("ChannelId")]
        public Channel Channel { get; set; }

        public SearchHistory()
        {
            SearchDate = DateTime.UtcNow;
        }
    }
}
