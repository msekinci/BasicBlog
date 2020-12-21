using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MSEkinci.BasicBlog.Business.Interfaces;
using MSEkinci.BasicBlog.DTO.DTOs.BlogDTOs;
using MSEkinci.BasicBlog.DTO.DTOs.CategoryBlogDTOs;
using MSEkinci.BasicBlog.DTO.DTOs.CategoryDTOs;
using MSEkinci.BasicBlog.Entities.Concrete;
using MSEkinci.BasicBlog.WebApi.CustomFilters;
using MSEkinci.BasicBlog.WebApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MSEkinci.BasicBlog.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogsController : BaseController
    {
        private readonly IBlogService _blogService;
        private readonly IMapper _mapper;

        public BlogsController(IBlogService blogService, IMapper mapper)
        {
            _blogService = blogService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(_mapper.Map<List<BlogListDTO>>(await _blogService.GetAllSortedByPostedTimeAsync()));
        }

        [HttpGet("{id}")]
        [ServiceFilter(typeof(ValidId<Blog>))]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(_mapper.Map<BlogListDTO>(await _blogService.FindByIdAsyc(id)));
        }

        [HttpPost]
        [Authorize]
        [ValidModel]
        public async Task<IActionResult> Add([FromForm] BlogAddModel blogAddModel)
        {
            var uploadModel = await UploadFileAsync(blogAddModel.Image, "image/jpeg");
            if (uploadModel.UploadState == Enums.UploadState.Success)
            {
                blogAddModel.ImagePath = uploadModel.NewName;
                await _blogService.AddAsync(_mapper.Map<Blog>(blogAddModel));
                return Created("", blogAddModel);
            }
            else if (uploadModel.UploadState == Enums.UploadState.NotExist)
            {
                await _blogService.AddAsync(_mapper.Map<Blog>(blogAddModel));
                return Created("", blogAddModel);
            }
            else
            {
                return BadRequest(uploadModel.ErrorMessage);
            }
        }

        [HttpPut("{id}")]
        [Authorize]
        [ValidModel]
        [ServiceFilter(typeof(ValidId<Blog>))]
        public async Task<IActionResult> Update(int id, [FromForm] BlogUpdateModel blogUpdateModel)
        {
            if (id != blogUpdateModel.Id)
            {
                return BadRequest("Invalid ID");
            }

            var uploadModel = await UploadFileAsync(blogUpdateModel.Image, "image/jpeg");
            if (uploadModel.UploadState == Enums.UploadState.Success)
            {
                var updatedBlog = await _blogService.FindByIdAsyc(blogUpdateModel.Id);
                updatedBlog.ShortDescription = blogUpdateModel.ShortDescription;
                updatedBlog.Description = blogUpdateModel.Description;
                updatedBlog.Title = blogUpdateModel.Title;
                updatedBlog.ImagePath = uploadModel.NewName;
                await _blogService.UpdateAsync(updatedBlog);
                return NoContent();
            }
            else if (uploadModel.UploadState == Enums.UploadState.NotExist)
            {
                var updatedBlog = await _blogService.FindByIdAsyc(blogUpdateModel.Id);
                updatedBlog.ShortDescription = blogUpdateModel.ShortDescription;
                updatedBlog.Description = blogUpdateModel.Description;
                updatedBlog.Title = blogUpdateModel.Title;
                await _blogService.UpdateAsync(updatedBlog);
                return NoContent();
            }
            else
            {
                return BadRequest(uploadModel.ErrorMessage);
            }
        }

        [HttpDelete("{id}")]
        [Authorize]
        [ServiceFilter(typeof(ValidId<Blog>))]
        public async Task<IActionResult> Delete(int id)
        {
            await _blogService.RemoveAsync(new Blog { Id = id });
            return NoContent();
        }

        [HttpPost("[action]")]
        [Authorize]
        public async Task<IActionResult> AddToCategory(CategoryBlogDTO categoryBlogDTO)
        {
            await _blogService.AddToCategoryAsync(categoryBlogDTO);
            return Created("", categoryBlogDTO);
        }

        [HttpPost("[action]")]
        [Authorize]
        public async Task<IActionResult> RemoveFromCategory(CategoryBlogDTO categoryBlogDTO)
        {
            await _blogService.RemoveFromCategoryAsync(categoryBlogDTO);
            return NoContent();
        }

        [HttpGet("[action]/{id}")]
        [ServiceFilter(typeof(ValidId<Category>))]
        public async Task<IActionResult> GetAllByCategoryId(int id)
        {
            return Ok(_mapper.Map<CategoryListDTO>(await _blogService.GetAllByCategoryId(id)));
        }

        [HttpGet("{id}/[action]")]
        [ServiceFilter(typeof(ValidId<Blog>))]
        public async Task<IActionResult> GetCategories(int id)
        {
            return Ok(await _blogService.GetCategoriesByBlogIdAsync(id));
        }

        [HttpGet("[action]")]
        [ServiceFilter(typeof(ValidId<Blog>))]
        public async Task<IActionResult> GetLastFiveBlogs()
        {
            return Ok(_mapper.Map<BlogListDTO>(await _blogService.GetLastFiveBlogsAsync()));
        }
    }
}
