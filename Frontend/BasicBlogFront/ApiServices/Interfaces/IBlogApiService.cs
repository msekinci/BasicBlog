using BasicBlogFront.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BasicBlogFront.ApiServices.Interfaces
{
    public interface IBlogApiService
    {
        Task<List<BlogListModel>> GetAllAsync();
        Task<List<BlogListModel>> GetAllByCategoryIdAsync(int categoryId);
        Task<BlogListModel> GetByIdAsync(int id);
        Task AddAsync(BlogAddModel blogAddModel);
        Task UpdateAsync(BlogUpdateModel blogUpdateModel);
        Task DeleteAsync(int id);
    }
}
