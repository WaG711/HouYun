using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HouYun3.Controllers
{
    public class AdminPanelController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
