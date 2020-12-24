using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using MSEkinci.BasicBlog.Business.Interfaces;
using MSEkinci.BasicBlog.Business.Tools.FacadeTool;
using MSEkinci.BasicBlog.DTO.DTOs.BlogDTOs;
using MSEkinci.BasicBlog.DTO.DTOs.CategoryBlogDTOs;
using MSEkinci.BasicBlog.DTO.DTOs.CategoryDTOs;
using MSEkinci.BasicBlog.DTO.DTOs.CommentDTOs;
using MSEkinci.BasicBlog.Entities.Concrete;
using MSEkinci.BasicBlog.WebApi.CustomFilters;
using MSEkinci.BasicBlog.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MSEkinci.BasicBlog.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogsController : BaseController
    {
        private readonly IBlogService _blogService;
        private readonly ICommentService _commentService;
        private readonly IMapper _mapper;
        private readonly IFacade _facade;

        public BlogsController(IBlogService blogService, ICommentService commentService, IMapper mapper, IFacade facade)
        {
            _blogService = blogService;
            _commentService = commentService;
            _facade = facade;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            if (_facade.MemoryCache.TryGetValue("blogListAll", out List<BlogListDTO> bl))
            {
                return Ok(bl);
            }

            var blogList = _mapper.Map<List<BlogListDTO>>(await _blogService.GetAllSortedByPostedTimeAsync());
            _facade.MemoryCache.Set("blogListAll", blogList, new MemoryCacheEntryOptions()
            {
                AbsoluteExpiration = DateTime.Now.AddDays(1),
                Priority = CacheItemPriority.Normal
            });
            return Ok(blogList);
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
            await _blogService.RemoveAsync(await _blogService.FindByIdAsyc(id));
            return NoContent();
        }

        [HttpPost("[action]")]
        [Authorize]
        public async Task<IActionResult> AddToCategory(CategoryBlogDTO categoryBlogDTO)
        {
            await _blogService.AddToCategoryAsync(categoryBlogDTO);
            return Created("", categoryBlogDTO);
        }

        [HttpDelete("[action]")]
        [Authorize]
        public async Task<IActionResult> RemoveFromCategory([FromQuery]CategoryBlogDTO categoryBlogDTO)
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
        public async Task<IActionResult> GetLastFiveBlogs()
        {
            return Ok(_mapper.Map<List<BlogListDTO>>(await _blogService.GetLastFiveBlogsAsync()));
        }

        [HttpGet("{id}/[action]")]
        [ServiceFilter(typeof(ValidId<Blog>))]
        public async Task<IActionResult> Comments([FromRoute]int id, [FromQuery]int? parentCommentId)
        {
            return Ok(_mapper.Map<List<CommentListDTO>>(await _commentService.GetAllWithSubCommentsAsync(id, parentCommentId)));
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> Search([FromQuery]string s)
        {
            return Ok(_mapper.Map<List<BlogListDTO>>(await _blogService.SearchAsync(s)));
        }

        [HttpPost("[action]")]
        [ValidModel]
        public async Task<IActionResult> AddComment(CommentAddDTO commentAddDTO)
        {
            commentAddDTO.PostedTime = DateTime.Now;
            await _commentService.AddAsync(_mapper.Map<Comment>(commentAddDTO));
            return Created("", commentAddDTO);
        }
    }
}
