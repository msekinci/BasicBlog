using BasicBlogFront.Filters;
using Microsoft.AspNetCore.Mvc;

namespace BasicBlogFront.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        [JwtAuthorize]
        public IActionResult Index()
        {
            TempData["active"] = "home";
            return View();
        }
        public IActionResult SignOut()
        {
            HttpContext.Session.Remove("token");
            return RedirectToAction("Index", "Home", new { area = "" });
        }
    }
}
