using HouYun3.Models;

namespace HouYun3.ViewModels
{
    public class UploadViewModel
    {
        public Video Video { get; set; }
        public IEnumerable<Category> Categories { get; set; }
    }
}
