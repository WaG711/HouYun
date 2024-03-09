using HouYun.Models;

namespace HouYun.ViewModels.forVideo
{
    public class SearchViewModel
    {
        public string SearchTerm { get; set; }
        public IEnumerable<Video> Videos { get; set; }
    }
}
