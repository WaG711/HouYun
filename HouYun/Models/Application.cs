using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HouYun.Models
{
    public class Application
    {
        public Guid ApplicationId { get; set; }

        public bool IsActive { get; set; }

        [StringLength(150, MinimumLength = 3, ErrorMessage = "Минимальная длина 3 символа")]
        public string? FullName { get; set; }

        [StringLength(200, MinimumLength = 3, ErrorMessage = "Минимальная длина 3 символа")]
        public string? PlaceOfWork { get; set; }

        [StringLength(500, MinimumLength = 3, ErrorMessage = "Минимальная длина 3 символа")]
        public string? Thesis { get; set; }

        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }
    }
}
