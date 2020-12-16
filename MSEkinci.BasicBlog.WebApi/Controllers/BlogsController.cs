using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MSEkinci.BasicBlog.Business.Interfaces;
using MSEkinci.BasicBlog.DTO.DTOs.BlogDTOs;
using MSEkinci.BasicBlog.Entities.Concrete;
using MSEkinci.BasicBlog.WebApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace MSEkinci.BasicBlog.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogsController : ControllerBase
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
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(_mapper.Map<BlogListDTO>(await _blogService.FindByIdAsyc(id)));
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromForm]BlogAddModel blogAddModel)
        {
            if (blogAddModel.Image != null)
            {
                if (blogAddModel.Image.ContentType != "image/jpeg")
                {
                    return BadRequest("Invalid file type");
                }
                var fileName = Guid.NewGuid() + Path.GetExtension(blogAddModel.Image.FileName);
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img", fileName);
                var stream = new FileStream(path, FileMode.Create);
                await blogAddModel.Image.CopyToAsync(stream);
                blogAddModel.ImagePath = fileName;
            }
            
            await _blogService.AddAsync(_mapper.Map<Blog>(blogAddModel));
            return Created("", blogAddModel);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromForm]BlogUpdateModel blogUpdateModel)
        {
            if (id != blogUpdateModel.Id)
            {
                return BadRequest("Invalid ID");
            }

            if (blogUpdateModel.Image != null)
            {
                if (blogUpdateModel.Image.ContentType != "image/jpeg")
                {
                    return BadRequest("Invalid file type");
                }
                var fileName = Guid.NewGuid() + Path.GetExtension(blogUpdateModel.Image.FileName);
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img", fileName);
                var stream = new FileStream(path, FileMode.Create);
                await blogUpdateModel.Image.CopyToAsync(stream);
                blogUpdateModel.ImagePath = fileName;
            }

            await _blogService.UpdateAsync(_mapper.Map<Blog>(blogUpdateModel));
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _blogService.RemoveAsync(new Blog { Id = id });
            return NoContent();
        }
    }
}
