using HouYun3.IRepositories;
using Microsoft.AspNetCore.Mvc;

namespace HouYun3.Controllers
{
    public class ViewController : Controller
    {
        private readonly IViewRepository _viewRepository;

        public ViewController(IViewRepository viewRepository)
        {
            _viewRepository = viewRepository;
        }
    }
}
