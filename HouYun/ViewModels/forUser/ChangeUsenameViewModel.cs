using System.ComponentModel.DataAnnotations;

namespace HouYun.ViewModels.forUser
{
    public class ChangeUsenameViewModel
    {
        [Required]
        [Display(Name = "Новый Никнейм")]
        public string NewUsername { get; set; }
        [Required]
        [Display(Name = "Подтверждение поролем")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
