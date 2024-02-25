using System.ComponentModel.DataAnnotations;

namespace HouYun.ViewModels.forUser
{
    public class RegisterViewModel
    {
        [EmailAddress]
        public string Email { get; set; }

        [MaxLength(20)]
        [MinLength(3)]
        public string UserName { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}