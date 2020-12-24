using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MSEkinci.BasicBlog.Business.Tools.FacadeTool;

namespace MSEkinci.BasicBlog.WebApi.Controllers
{
    public class ErrorController : Controller
    {
        private readonly IFacade _facade;

        public ErrorController(IFacade facade)
        {
            _facade = facade;
        }

        [Route("/Error")]
        [HttpGet]
        public IActionResult Error()
        {
            var errorInfo = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            _facade.CustomLogger.LogError($"\nHatanın oluştuğu yer:{errorInfo.Path}\nHata Mesajı: {errorInfo.Error.Message}\nStack Trace:{errorInfo.Error.StackTrace}");
            return Problem(detail: "An error occured, we have started work on it!");
        }
    }
}
