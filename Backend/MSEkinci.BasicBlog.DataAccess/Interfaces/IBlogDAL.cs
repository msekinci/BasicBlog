using MSEkinci.BasicBlog.Entities.Concrete;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MSEkinci.BasicBlog.DataAccess.Interfaces
{
    public interface IBlogDAL : IGenericDAL<Blog>
    {
        Task<List<Blog>> GetAllByCategoryId(int categoryId);
        Task<List<Category>> GetCategoriesByBlogIdAsync(int blogId);
        Task<List<Blog>> GetLastFiveBlogsAsync();
    }
}
