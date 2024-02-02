using System.ComponentModel.DataAnnotations;

namespace HouYun3.Models
{
    public class User
    {
        [Key]
        public int UserID { get; set; }

        [Required(ErrorMessage = "Поле 'Email' обязательно для заполнения")]
        [EmailAddress(ErrorMessage = "Некорректный формат 'Email'")]
        public string Email { get; set; }

        [Display(Name = "Аватар")]
        public string Avatar { get; set; }

        [Required(ErrorMessage = "Поле 'Логин' обязательно для заполнения")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Длина 'Логина' должна быть от 3 до 50 символов")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Поле 'Пароль' обязательно для заполнения")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [ScaffoldColumn(false)]
        [Display(Name = "Дата регистрации")]
        public DateTime RegistrationDate { get; set; }

        public ICollection<Video> Videos { get; set; } = new List<Video>();
        public ICollection<SearchHistory> SearchHistory { get; set; } = new List<SearchHistory>();
        public ICollection<WatchHistory> WatchHistory { get; set; } = new List<WatchHistory>();
        public ICollection<WatchLater> WatchLaterList { get; set; } = new List<WatchLater>();
        public ICollection<Notification> Notifications { get; set; } = new List<Notification>();


        public User()
        {
            RegistrationDate = DateTime.Now;
        }
    }
}
