using HouYun.Models;
using System.ComponentModel.DataAnnotations;

namespace HouYun.ViewModels.forUser
{
    public class ChangeRoleViewModel
    {
        [Required(ErrorMessage = "ФИО обязательно для заполнения")]
        [StringLength(150, MinimumLength = 3, ErrorMessage = "Минимальная длина 3 символа")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Место работы обязательно для заполнения")]
        [StringLength(200, MinimumLength = 3, ErrorMessage = "Минимальная длина 3 символа")]
        public string PlaceOfWork { get; set; }

        [Required(ErrorMessage = "Тезис обязателен для заполнения")]
        [StringLength(500, MinimumLength = 3, ErrorMessage = "Минимальная длина 3 символа")]
        public string Thesis { get; set; }

        public User? User { get; set; }
    }
}
