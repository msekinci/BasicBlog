using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MSEkinci.BasicBlog.Business.Tools.LogTool;

namespace MSEkinci.BasicBlog.WebApi.Controllers
{
    public class ErrorController : Controller
    {
        private readonly ICustomLogger _customLogger;

        public ErrorController(ICustomLogger customLogger)
        {
            _customLogger = customLogger;
        }

        [Route("/Error")]
        public IActionResult Error()
        {
            var errorInfo = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            _customLogger.LogError($"\nHatanın oluştuğu yer:{errorInfo.Path}\nHata Mesajı: {errorInfo.Error.Message}\nStack Trace:{errorInfo.Error.StackTrace}");
            return Problem(detail: "An error occured, we have started work on it!");
        }
    }
}
