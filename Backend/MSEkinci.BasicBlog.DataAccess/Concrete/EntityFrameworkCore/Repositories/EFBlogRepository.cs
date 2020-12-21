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
        public async Task<List<Blog>> GetAllByCategoryId(int categoryId)
        {
            using var context = new BlogContext();
            return await context.Blogs.Join(context.CategoryBlogs, b => b.Id, cb => cb.BlogId,
                (blog, categoryBlog) => new
                {
                    blog = blog,
                    categoryBlog = categoryBlog
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
            using var context = new BlogContext();
            return await context.Categories.Join(context.CategoryBlogs, c => c.Id, cb => cb.CategoryId, (category, categoryBlog) =>
            new
            {
                category = category,
                categoryBlog = categoryBlog
            }).Where(x => x.categoryBlog.BlogId == blogId).Select(x => new Category
            {
                Id = x.category.Id,
                Name = x.category.Name
            }).ToListAsync();
        }

        public async Task<List<Blog>> GetLastFiveBlogsAsync()
        {
            using var context = new BlogContext();
            return await context.Blogs.OrderByDescending(x => x.PostedTime).Take(5).ToListAsync();
        }
    }
}
