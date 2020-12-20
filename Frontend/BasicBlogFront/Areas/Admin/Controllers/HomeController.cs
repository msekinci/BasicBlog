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
            return View();
        }
    }
}
