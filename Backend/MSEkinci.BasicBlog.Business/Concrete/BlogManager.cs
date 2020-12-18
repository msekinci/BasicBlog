using MSEkinci.BasicBlog.Business.Interfaces;
using MSEkinci.BasicBlog.DataAccess.Interfaces;
using MSEkinci.BasicBlog.DTO.DTOs.CategoryBlogDTOs;
using MSEkinci.BasicBlog.Entities.Concrete;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MSEkinci.BasicBlog.Business.Concrete
{
    public class BlogManager : GenericManager<Blog>, IBlogService
    {
        private readonly IGenericDAL<Blog> _genericDAL;
        private readonly IGenericDAL<CategoryBlog> _categoryBlogDAL;
        public BlogManager(IGenericDAL<Blog> genericDAL, IGenericDAL<CategoryBlog> categoryBlogDAL) : base(genericDAL)
        {
            _genericDAL = genericDAL;
            _categoryBlogDAL = categoryBlogDAL;
        }

        public async Task AddToCategoryAsync(CategoryBlogDTO categoryBlogDTO)
        {
            var categoryBlog = await _categoryBlogDAL.GetAsync(x =>
            x.BlogId == categoryBlogDTO.BlogId &&
            x.CategoryId == categoryBlogDTO.CategoryId);

            if (categoryBlog == null)
            {
                await _categoryBlogDAL.AddAsync(
                new CategoryBlog
                {
                    BlogId = categoryBlogDTO.BlogId,
                    CategoryId = categoryBlogDTO.CategoryId
                });
            }
        }

        public async Task<List<Blog>> GetAllSortedByPostedTimeAsync()
        {
            return await _genericDAL.GetAllAsync(x => x.PostedTime);
        }

        public async Task RemoveFromCategoryAsync(CategoryBlogDTO categoryBlogDTO)
        {
            var categoryBlog = await _categoryBlogDAL.GetAsync(x =>
            x.BlogId == categoryBlogDTO.BlogId &&
            x.CategoryId == categoryBlogDTO.CategoryId);

            if (categoryBlog != null)
            {
                await _categoryBlogDAL.RemoveAsync(categoryBlog);
            }
        }
    }
}
