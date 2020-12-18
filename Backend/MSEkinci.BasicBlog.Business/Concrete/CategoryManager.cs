using MSEkinci.BasicBlog.Business.Interfaces;
using MSEkinci.BasicBlog.DataAccess.Interfaces;
using MSEkinci.BasicBlog.Entities.Concrete;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MSEkinci.BasicBlog.Business.Concrete
{
    public class CategoryManager : GenericManager<Category>, ICategoryService
    {
        private readonly IGenericDAL<Category> _genericDAL;
        private readonly ICategoryDAL _categoryDAL;
        public CategoryManager(IGenericDAL<Category> genericDAL, ICategoryDAL categoryDAL) : base(genericDAL)
        {
            _genericDAL = genericDAL;
            _categoryDAL = categoryDAL;
        }

        public async Task<List<Category>> GetAllSortedByIdAsync()
        {
            return await _genericDAL.GetAllAsync(x => x.Id);
        }

        public async Task<List<Category>> GetAllWithCategoryBlogsAsync()
        {
            return await _categoryDAL.GetAllWithCategoryBlogsAsync();
        }
    }
}
