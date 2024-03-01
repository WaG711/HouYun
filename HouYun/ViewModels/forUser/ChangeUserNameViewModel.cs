using System.ComponentModel.DataAnnotations;

namespace HouYun.ViewModels.forUser
{
    public class ChangeUserNameViewModel
    {
        [Required]
        [StringLength(15, MinimumLength = 4)]
        public string NewUserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
