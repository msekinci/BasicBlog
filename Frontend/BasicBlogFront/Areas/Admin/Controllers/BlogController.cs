using BasicBlogFront.ApiServices.Interfaces;
using BasicBlogFront.Filters;
using BasicBlogFront.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BasicBlogFront.Areas.Admin.Controllers
{
    [Area("Admin")]
    [JwtAuthorize]
    public class BlogController : Controller
    {
        private readonly IBlogApiService _blogApiService;
        public BlogController(IBlogApiService blogApiService)
        {
            _blogApiService = blogApiService;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _blogApiService.GetAllAsync());
        }

        public IActionResult Create()
        {
            return View(new BlogAddModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(BlogAddModel blogAddModel)
        {
            if (ModelState.IsValid)
            {
                await _blogApiService.AddAsync(blogAddModel);
                return RedirectToAction("Index");
            }
            return View(new BlogAddModel());
        }
    }
}
