using Microsoft.AspNetCore.Mvc.Rendering;

namespace HouYun3.Models
{
    public class VideoViewModel
    {
        public IEnumerable<SelectListItem> Categories { get; set; }
        public IEnumerable<Video> Videos { get; set; }
        public string CategoryName { get; set; }
    }
}
