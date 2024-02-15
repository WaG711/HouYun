using HouYun.Models;

namespace HouYun.ViewModels.forVideo
{
    public class VideoViewModel
    {
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<Video> Videos { get; set; }
    }
}
