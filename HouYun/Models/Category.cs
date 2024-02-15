using System.ComponentModel.DataAnnotations;

namespace HouYun.Models
{
    public class Category
    {
        [Key]
        public Guid CategoryId { get; set; }

        [Required(ErrorMessage = "Поле 'Название' обязательно для заполнения")]
        [StringLength(50, ErrorMessage = "Длина 'Названия' не должна превышать 50 символов")]
        public string Name { get; set; }

        public ICollection<Video> Videos { get; set; }

        public Category()
        {
            Videos = new List<Video>();
        }
    }
}
