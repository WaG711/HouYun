using System.Collections.Generic;
using HouYun3.Models;

namespace HouYun3.ViewModels.forVideo
{
    public class ChannelViewModel
    {
        public string ChannelName { get; set; }
        public IEnumerable<Video> Videos { get; set; }
    }
}
