using System.ComponentModel.DataAnnotations;

namespace HouYun3.ViewModels.forUser
{
    public class ChangeChannelNameandDescriptionViewModel
    {
        [Required]
        [Display(Name = "Имя канала")]
        public string ChannelName { get; set; }

        public string Description { get; set; }
    }
}
