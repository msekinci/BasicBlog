using MSEkinci.BasicBlog.Business.Interfaces;
using MSEkinci.BasicBlog.DataAccess.Interfaces;
using MSEkinci.BasicBlog.Entities.Concrete;

namespace MSEkinci.BasicBlog.Business.Concrete
{
    public class CategoryManager : GenericManager<Category>, ICategoryService
    {
        private readonly IGenericDAL<Category> _genericDAL;
        public CategoryManager(IGenericDAL<Category> genericDAL) : base(genericDAL)
        {
            _genericDAL = genericDAL;
        }
    }
}
