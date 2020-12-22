using BasicBlogFront.ApiServices.Interfaces;
using BasicBlogFront.Models;
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
        public async Task<IActionResult> Index(int? category, string s)
        {
            if (category.HasValue)
            {
                ViewBag.ActiveCategory = category;
                return View(await _blogApiService.GetAllByCategoryIdAsync((int)category));
            }
            if (!string.IsNullOrWhiteSpace(s))
            {
                ViewBag.SearchString = s;
                return View(await _blogApiService.SearchAsync(s));
            }
            return View(await _blogApiService.GetAllAsync());
        }

        public async Task<IActionResult> BlogDetail(int id)
        {
            ViewBag.Comments = await _blogApiService.GetCommentsAsync(id, null);
            return View(await _blogApiService.GetByIdAsync(id));
        }

        public async Task<IActionResult> AddToComment(CommentAddModel commentAddModel)
        {
            await _blogApiService.AddToComment(commentAddModel);
            return RedirectToAction("BlogDetail", new { id = commentAddModel.BlogId });
        }
    }
}
