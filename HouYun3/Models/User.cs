using HouYun3.Models;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace HouYun3.Models
{
    public class User : IdentityUser
    {
        [Key]
        public override string Id { get; set; }

        [Required(ErrorMessage = "Поле 'Email' обязательно для заполнения")]
        [EmailAddress(ErrorMessage = "Некорректный формат 'Email'")]
        public override string Email { get; set; }

        [Required(ErrorMessage = "Поле 'Логин' обязательно для заполнения")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Длина 'Логина' должна быть от 3 до 50 символов")]
        public override string UserName { get; set; }

        [ScaffoldColumn(false)]
        [Display(Name = "Дата регистрации")]
        public DateTime RegistrationDate { get; set; }

        public ICollection<Video> Videos { get; set; } = new List<Video>();
        public ICollection<SearchHistory> SearchHistory { get; set; } = new List<SearchHistory>();
        public ICollection<WatchHistory> WatchHistory { get; set; } = new List<WatchHistory>();
        public ICollection<WatchLater> WatchLaterList { get; set; } = new List<WatchLater>();
        public ICollection<Notification> Notifications { get; set; } = new List<Notification>();
    }
}
