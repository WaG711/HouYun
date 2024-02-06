using HouYun3.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HouYun3.ViewModels
{
    public class UploadViewModel
    {
        public Video Video { get; set; }
        public IEnumerable<SelectListItem> Categories { get; set; }
    }
}
