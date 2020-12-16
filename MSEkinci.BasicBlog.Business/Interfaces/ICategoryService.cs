using MSEkinci.BasicBlog.Entities.Concrete;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MSEkinci.BasicBlog.Business.Interfaces
{
    public interface ICategoryService : IGenericService<Category>
    {
        Task<List<Category>> GetAllSortedById();
    }
}
