using System.ComponentModel.DataAnnotations;
using HouYun.Models;

namespace HouYun.ViewModels.forUser
{
    public class UpdateChannelViewModel
    {
        [StringLength(15, MinimumLength = 4)]
        [Display(Name = "Имя канала")]
        public string? ChannelName { get; set; }

        [StringLength(150)]
        [Display(Name = "Описание")]
        public string? Description { get; set; }

    }
}
