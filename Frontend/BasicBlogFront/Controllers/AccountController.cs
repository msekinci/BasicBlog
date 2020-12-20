using BasicBlogFront.ApiServices.Interfaces;
using BasicBlogFront.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BasicBlogFront.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAuthApiService _authApiService;    
        public AccountController(IAuthApiService authApiService)
        {
            _authApiService = authApiService;
        }
        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(AppUserLoginModel appUserLoginModel)
        {
            if (await _authApiService.SignIn(appUserLoginModel))
            {
                return RedirectToAction("Index", "Home", new { @area="Admin" });
            }
            return View(appUserLoginModel);
        }
    }
}
