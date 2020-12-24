using BasicBlogFront.ApiServices.Interfaces;
using BasicBlogFront.Filters;
using BasicBlogFront.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
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
            TempData["active"] = "blog";
            return View(await _blogApiService.GetAllAsync());
        }

        public IActionResult Create()
        {
            TempData["active"] = "blog";
            return View(new BlogAddModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(BlogAddModel blogAddModel)
        {
            TempData["active"] = "blog";
            if (ModelState.IsValid)
            {
                await _blogApiService.AddAsync(blogAddModel);
                return RedirectToAction("Index");
            }
            return View(new BlogAddModel());
        }

        public async Task<IActionResult> Update(int id)
        {
            TempData["active"] = "blog";
            var blog = await _blogApiService.GetByIdAsync(id);
            return View(new BlogUpdateModel
            {
                Id = blog.Id,
                Title = blog.Title,
                ShortDescription = blog.ShortDescription,
                Description = blog.Description
            });
        }

        [HttpPost]
        public async Task<IActionResult> Update(BlogUpdateModel blogUpdateModel)
        {
            TempData["active"] = "blog";
            if (ModelState.IsValid)
            {
                await _blogApiService.UpdateAsync(blogUpdateModel);
                return RedirectToAction("Index");
            }
            return View(blogUpdateModel);
        }

        public async Task<IActionResult> Delete(int id)
        {
            TempData["active"] = "blog";
            await _blogApiService.DeleteAsync(id);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> AssignCategory(int id, [FromServices] ICategoryApiService categoryApiService)
        {
            TempData["active"] = "blog";
            var categories = await categoryApiService.GetAllAsync();
            var blogCategories = (await _blogApiService.GetCategoriesAsync(id)).Select(x => x.Name);
            TempData["blogId"] = id;
            List<AssignCategoryModel> list = new List<AssignCategoryModel>();

            foreach (var category in categories)
            {
                AssignCategoryModel model = new AssignCategoryModel();
                model.CategoryId = category.Id;
                model.CategoryName = category.Name;
                model.Exists = blogCategories.Contains(category.Name);

                list.Add(model);
            }
            return View(list);
        }
        [HttpPost]
        public async Task<IActionResult> AssignCategory(List<AssignCategoryModel> list)
        {
            TempData["active"] = "blog";
            int id = (int)TempData["blogId"];
            foreach (var item in list)
            {
                if (item.Exists)
                {
                    CategoryBlogModel model = new CategoryBlogModel();
                    model.BlogId = id;
                    model.CategoryId = item.CategoryId;
                    await _blogApiService.AddToCategoryAsync(model);
                }
                else
                {
                    CategoryBlogModel model = new CategoryBlogModel();
                    model.BlogId = id;
                    model.CategoryId = item.CategoryId;
                    await _blogApiService.RemoveFromCategoryAsync(model);
                }
            }
            return RedirectToAction("Index");
        }
    }
}
