using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HouYun3.Models
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

        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }

        public SearchHistory()
        {
            SearchDate = DateTime.UtcNow;
        }
    }
}
