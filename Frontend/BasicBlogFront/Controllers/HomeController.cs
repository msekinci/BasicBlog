using BasicBlogFront.ApiServices.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BasicBlogFront.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBlogApiService _blogApiService;
        public HomeController(IBlogApiService blogApiService)
        {
            _blogApiService = blogApiService;
        }
        public async Task<IActionResult> Index(int? category)
        {
            if (category.HasValue)
            {
                ViewBag.ActiveCategory = category;
                return View(await _blogApiService.GetAllByCategoryIdAsync(category.Value));
            }
            else
            {
                return View(await _blogApiService.GetAllAsync());
            }
        }

        public async Task<IActionResult> BlogDetail(int id)
        {
            return View(await _blogApiService.GetByIdAsync(id));
        }
    }
}
