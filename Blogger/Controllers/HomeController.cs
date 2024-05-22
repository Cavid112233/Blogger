using Microsoft.AspNetCore.Mvc;

namespace Blogger.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
