using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HouYun3.Models
{
    public class Channel
    {
        public Guid ChannelId { get; set; }

        [Required(ErrorMessage = "Поле 'Название канала' обязательно для заполнения")]
        [Display(Name = "Название канала")]
        public string Name { get; set; }

        [ScaffoldColumn(false)]
        [Display(Name = "Дата регистрации")]
        public DateTime RegistrationDate  { get; set; }

        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }

        public ICollection<Subscription> Subscribers { get; set; }

        public Channel()
        {
            Subscribers = new List<Subscription>();
            RegistrationDate = DateTime.UtcNow;
        }
    }
}
