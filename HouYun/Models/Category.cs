using System.ComponentModel.DataAnnotations;

namespace HouYun.Models
{
    public class Category
    {
        [Key]
        public Guid CategoryId { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        public ICollection<Video> Videos { get; set; }

        public Category()
        {
            Videos = new List<Video>();
        }
    }
}
