using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace HouYun.Models
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

        public Channel Channel { get; set; }
        public ICollection<Subscription> Subscriptions { get; set; }

        public User()
        {
            Subscriptions = new List<Subscription>();
            Channel = new Channel();
        }
    }
}
