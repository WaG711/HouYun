using System.ComponentModel.DataAnnotations;
using HouYun3.Models;

namespace HouYun3.ViewModels.forUser
{
    public class UpdateChannelViewModel
    {
        [Display(Name = "Имя канала")]
        public string? ChannelName { get; set; }


        [Display(Name = "Описание")]
        public string? Description { get; set; }

    }
}
