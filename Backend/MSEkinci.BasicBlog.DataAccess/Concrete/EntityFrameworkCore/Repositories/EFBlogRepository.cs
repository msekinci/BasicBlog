using Microsoft.EntityFrameworkCore;
using MSEkinci.BasicBlog.DataAccess.Concrete.EntityFrameworkCore.Context;
using MSEkinci.BasicBlog.DataAccess.Interfaces;
using MSEkinci.BasicBlog.Entities.Concrete;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MSEkinci.BasicBlog.DataAccess.Concrete.EntityFrameworkCore.Repositories
{
    public class EFBlogRepository : EFGenericRepository<Blog>, IBlogDAL
    {
        private readonly BlogContext _context;
        public EFBlogRepository(BlogContext context) : base(context)
        {
            _context = context;
        }
        public async Task<List<Blog>> GetAllByCategoryId(int categoryId)
        {
            return await _context.Blogs.Join(_context.CategoryBlogs, b => b.Id, cb => cb.BlogId,
                (blog, categoryBlog) => new
                {
                    blog,
                    categoryBlog
                }).Where(x => x.categoryBlog.CategoryId == categoryId).Select(x => new Blog
                {
                    AppUser = x.blog.AppUser,
                    AppUserId = x.blog.AppUserId,
                    CategoryBlogs = x.blog.CategoryBlogs,
                    Comments = x.blog.Comments,
                    Description = x.blog.Description,
                    Id = x.blog.Id,
                    ImagePath = x.blog.ImagePath,
                    PostedTime = x.blog.PostedTime,
                    ShortDescription = x.blog.ShortDescription,
                    Title = x.blog.Title
                }).ToListAsync();
        }

        public async Task<List<Category>> GetCategoriesByBlogIdAsync(int blogId)
        {
            return await _context.Categories.Join(_context.CategoryBlogs, c => c.Id, cb => cb.CategoryId, (category, categoryBlog) =>
            new
            {
                category,
                categoryBlog
            }).Where(x => x.categoryBlog.BlogId == blogId).Select(x => new Category
            {
                Id = x.category.Id,
                Name = x.category.Name
            }).ToListAsync();
        }

        public async Task<List<Blog>> GetLastFiveBlogsAsync()
        {
            return await _context.Blogs.OrderByDescending(x => x.PostedTime).Take(5).ToListAsync();
        }
    }
}