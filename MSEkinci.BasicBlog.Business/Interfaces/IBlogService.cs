using MSEkinci.BasicBlog.Entities.Concrete;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MSEkinci.BasicBlog.Business.Interfaces
{
    public interface IBlogService : IGenericService<Blog>
    {
        Task<List<Blog>> GetAllSortedByPostedTimeAsync();
    }
}
