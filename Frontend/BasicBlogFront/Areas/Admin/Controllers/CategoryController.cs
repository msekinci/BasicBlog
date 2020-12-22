using BasicBlogFront.ApiServices.Interfaces;
using BasicBlogFront.Filters;
using BasicBlogFront.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BasicBlogFront.Areas.Admin.Controllers
{
    [Area("Admin")]
    [JwtAuthorize]
    public class CategoryController : Controller
    {
        private readonly ICategoryApiService _categoryApiService;
        public CategoryController(ICategoryApiService categoryApiService)
        {
            _categoryApiService = categoryApiService;
        }
        public async Task<IActionResult> Index()
        {
            TempData["active"] = "category";
            return View(await _categoryApiService.GetAllAsync());
        }

        public IActionResult Create()
        {
            TempData["active"] = "category";
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CategoryAddModel categoryAddModel)
        {
            TempData["active"] = "category";
            if (ModelState.IsValid)
            {
                await _categoryApiService.AddAsync(categoryAddModel);
                return RedirectToAction("Index");
            }
            return View(categoryAddModel);
        }

        public async Task<IActionResult> Update(int id)
        {
            TempData["active"] = "category";
            var category = await _categoryApiService.GetByIdAsync(id);
            return View(new CategoryUpdateModel 
            {
                Id = category.Id,
                Name = category.Name
            });
        }

        [HttpPost]
        public async Task<IActionResult> Update(CategoryUpdateModel categoryUpdateModel)
        {
            TempData["active"] = "category";
            if (ModelState.IsValid)
            {
                await _categoryApiService.UpdateAsync(categoryUpdateModel);
                return RedirectToAction("Index");
            }
            return View(categoryUpdateModel);
        }

        public async Task<IActionResult> Delete(int id)
        {
            TempData["active"] = "category";
            await _categoryApiService.DeleteAsync(id);
            return RedirectToAction("Index");
        }
    }
}
