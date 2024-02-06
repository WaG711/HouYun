using System.ComponentModel.DataAnnotations;
using HouYun3.ApplicationModel;

namespace HouYun3.Models
{
    public class SearchHistory
    {
        [Key]
        public int SearchHistoryId { get; set; }

        [Required(ErrorMessage = "Поле 'Поисковый запрос' обязательно для заполнения")]
        [StringLength(100, ErrorMessage = "Длина 'Поискового запроса' не должна превышать 100 символов")]
        public string SearchQuery { get; set; }

        [ScaffoldColumn(false)]
        [Display(Name = "Дата поиска")]
        public DateTime SearchDate { get; set; }

        [Display(Name = "Пользователь")]
        public string UserId { get; set; }
        public User User { get; set; }

        public SearchHistory()
        {
            SearchDate = DateTime.UtcNow;
        }
    }
}
