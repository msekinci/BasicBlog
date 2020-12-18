using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MSEkinci.BasicBlog.Business.Interfaces;
using MSEkinci.BasicBlog.DTO.DTOs.CategoryDTOs;
using MSEkinci.BasicBlog.Entities.Concrete;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MSEkinci.BasicBlog.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public CategoriesController(ICategoryService categoryService, IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(_mapper.Map<List<CategoryListDTO>>(await _categoryService.GetAllSortedByIdAsync()));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(_mapper.Map<CategoryListDTO>(await _categoryService.FindByIdAsyc(id)));
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Add(CategoryAddDTO categoryAddDTO)
        {
            await _categoryService.AddAsync(_mapper.Map<Category>(categoryAddDTO));
            return Created("", categoryAddDTO);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Update(int id, CategoryUpdateDTO categoryUpdateDTO)
        {
            if (id != categoryUpdateDTO.Id)
            {
                return BadRequest("Invalid ID");
            }
            await _categoryService.UpdateAsync(_mapper.Map<Category>(categoryUpdateDTO));
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            await _categoryService.RemoveAsync(new Category { Id = id });
            return NoContent();
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetWithBlogsCount()
        {
            var categories = await _categoryService.GetAllWithCategoryBlogsAsync();
            List<CategoryWithBlogCount> listCategory = new List<CategoryWithBlogCount>();

            foreach (var category in categories)
            {
                CategoryWithBlogCount categoryWithBlogCount = new CategoryWithBlogCount
                {
                    Category = category,
                    BlogsCount = category.CategoryBlogs.Count
                };
                listCategory.Add(categoryWithBlogCount);
            }
            return Ok(listCategory);
        }
    }
}
