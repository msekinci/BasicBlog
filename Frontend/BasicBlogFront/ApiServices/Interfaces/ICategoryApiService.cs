using BasicBlogFront.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BasicBlogFront.ApiServices.Interfaces
{
    public interface ICategoryApiService
    {
        Task<List<CategoryListModel>> GetAllAsync();
        Task<CategoryListModel> GetByIdAsync(int id);
        Task<List<CategoryWithBlogCount>> GetAllWithBlogCountAsync();
        Task AddAsync(CategoryAddModel categoryAddModel);
        Task UpdateAsync(CategoryUpdateModel categoryUpdateModel);
        Task DeleteAsync(int id);
    }
}
