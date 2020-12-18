using Microsoft.EntityFrameworkCore;
using MSEkinci.BasicBlog.DataAccess.Concrete.EntityFrameworkCore.Context;
using MSEkinci.BasicBlog.DataAccess.Interfaces;
using MSEkinci.BasicBlog.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSEkinci.BasicBlog.DataAccess.Concrete.EntityFrameworkCore.Repositories
{
    public class EFCategoryRepository : EFGenericRepository<Category>, ICategoryDAL
    {
        public async Task<List<Category>> GetAllWithCategoryBlogsAsync()
        {
            using var context = new BlogContext();
            return await context.Categories.OrderByDescending(x => x.Id).Include(x => x.CategoryBlogs).ToListAsync();
        }
    }
}
