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
        Task<List<CommentListModel>> GetCommentsAsync(int blogId, int? parentComment);
        Task AddToComment(CommentAddModel commentAddModel);
        Task<List<CategoryListModel>> GetCategoriesAsync(int blogId);
        Task<List<BlogListModel>> GetLastFiveAsync();
        Task<List<BlogListModel>> SearchAsync(string s);
        Task AddToCategoryAsync(CategoryBlogModel model);
        Task RemoveFromCategoryAsync(CategoryBlogModel model);
    }
}
