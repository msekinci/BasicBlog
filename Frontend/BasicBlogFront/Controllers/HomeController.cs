using Microsoft.AspNetCore.Mvc;

namespace BasicBlogFront.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
