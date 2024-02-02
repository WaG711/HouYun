using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HouYun3.Models
{
    public class SearchHistory
    {
        [Key]
        public int SearchHistoryID { get; set; }

        [Required(ErrorMessage = "Поле 'Поисковый запрос' обязательно для заполнения")]
        [StringLength(100, ErrorMessage = "Длина 'Поискового запроса' не должна превышать 100 символов")]
        public string SearchQuery { get; set; }

        [ScaffoldColumn(false)]
        [Display(Name = "Дата поиска")]
        public DateTime SearchDate { get; set; }

        [Required(ErrorMessage = "Поле 'Пользователь ID' обязательно для заполнения")]
        public int UserID { get; set; }

        [ForeignKey("UserID")]
        public User User { get; set; }


        public SearchHistory()
        {
            SearchDate = DateTime.Now;
        }
    }
}
