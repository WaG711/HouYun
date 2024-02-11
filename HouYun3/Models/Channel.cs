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

        [Required(ErrorMessage = "Поле 'Описание канала' обязательно для заполнения")]
        [Display(Name = "Описание канала")]
        [StringLength(500, ErrorMessage = "Длина 'Описания' не должна превышать 500 символов")]
        public string Description { get; set; }

        [ScaffoldColumn(false)]
        [Display(Name = "Дата регистрации")]
        public DateTime RegistrationDate  { get; set; }

        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }

        public ICollection<Subscription> Subscribers { get; set; }
        public ICollection<Video> Videos { get; set; }

        public Channel()
        {
            Videos = new List<Video>();
            Subscribers = new List<Subscription>();
            RegistrationDate = DateTime.UtcNow;
        }
    }
}
