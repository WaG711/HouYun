using HouYun3.Models;

namespace HouYun3.ViewModels.forVideo
{
    public class VideoViewModel
    {
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<Video> Videos { get; set; }
    }
}
