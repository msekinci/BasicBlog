using BasicBlogFront.ApiServices.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BasicBlogFront.ViewComponents
{
    public class CategoryList : ViewComponent
    {
        private readonly ICategoryApiService _categoryApiService;
        public CategoryList(ICategoryApiService categoryApiService)
        {
            _categoryApiService = categoryApiService;
        }
        public IViewComponentResult Invoke()
        {
            //Cannot be async method in ViewComponant
            return View(_categoryApiService.GetAllWithBlogCountAsync().Result);
        }
    }
}
