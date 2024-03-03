using System.ComponentModel.DataAnnotations;

namespace HouYun.ViewModels.forUser
{
    public class RegistrationViewModel
    {
        [EmailAddress]
        public string Email { get; set; }

        [MaxLength(20)]
        [MinLength(4)]
        public string UserName { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}