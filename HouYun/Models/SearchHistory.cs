using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HouYun.Models
{
    public class SearchHistory
    {
        [Key]
        public Guid SearchHistoryId { get; set; }

        [Required(ErrorMessage = "Поле 'Поисковый запрос' обязательно для заполнения")]
        [StringLength(100, ErrorMessage = "Длина 'Поискового запроса' не должна превышать 100 символов")]
        public string SearchQuery { get; set; }

        [ScaffoldColumn(false)]
        [Display(Name = "Дата поиска")]
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
