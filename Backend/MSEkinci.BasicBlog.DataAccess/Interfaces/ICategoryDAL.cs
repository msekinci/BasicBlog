using MSEkinci.BasicBlog.Entities.Concrete;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MSEkinci.BasicBlog.DataAccess.Interfaces
{
    public interface ICategoryDAL : IGenericDAL<Category>
    {
        Task<List<Category>> GetAllWithCategoryBlogsAsync();
    }
}
