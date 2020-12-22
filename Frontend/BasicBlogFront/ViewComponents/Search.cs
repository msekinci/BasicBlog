using Microsoft.AspNetCore.Mvc;

namespace BasicBlogFront.ViewComponents
{
    public class Search : ViewComponent{
        public IViewComponentResult Invoke(string s){
            ViewBag.SearchString=s;
            return View();
        }
    }
}