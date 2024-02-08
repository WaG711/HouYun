using HouYun3.Models;

namespace HouYun3.ViewModels
{
    public class VideoViewModel
    {
        public IEnumerable<Video> Videos { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public string SelectedCategory { get; set; }
        public string SearchTerm { get; set; }
    }
}
