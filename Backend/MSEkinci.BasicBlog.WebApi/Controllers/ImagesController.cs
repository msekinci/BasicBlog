using Microsoft.AspNetCore.Mvc;
using MSEkinci.BasicBlog.Business.Interfaces;
using MSEkinci.BasicBlog.Entities.Concrete;
using MSEkinci.BasicBlog.WebApi.CustomFilters;
using System.Threading.Tasks;

namespace MSEkinci.BasicBlog.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IBlogService _blogService;
        public ImagesController(IBlogService blogService)
        {
            _blogService = blogService;
        }

        [HttpGet("[action]/{id}")]
        [ServiceFilter(typeof(ValidId<Blog>))]
        public async Task<IActionResult> GetBlogImageById(int id)
        {
            var blog = await _blogService.FindByIdAsyc(id);
            if (string.IsNullOrWhiteSpace(blog.ImagePath))
            {
                return NotFound("Image could not found");
            }
            return File($"/img/{blog.ImagePath}", "image/jpeg");
        }
    }
}
