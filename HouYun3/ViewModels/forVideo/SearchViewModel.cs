using HouYun3.Models;

namespace HouYun3.ViewModels.forVideo
{
    public class SearchViewModel
    {
        public string SearchTerm { get; set; }
        public IEnumerable<Video> Videos { get; set; }
        public IEnumerable<SearchHistory> LastSearches { get; set; }
    }
}
