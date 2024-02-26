using System.ComponentModel.DataAnnotations;

namespace HouYun.ViewModels.forUser
{
    public class ChangeUsenameViewModel
    {
        [Required]
        [StringLength(15, MinimumLength = 4)]
        public string NewUsername { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
