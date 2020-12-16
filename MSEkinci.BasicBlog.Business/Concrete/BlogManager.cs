using MSEkinci.BasicBlog.Business.Interfaces;
using MSEkinci.BasicBlog.DataAccess.Interfaces;
using MSEkinci.BasicBlog.Entities.Concrete;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MSEkinci.BasicBlog.Business.Concrete
{
    public class BlogManager : GenericManager<Blog>, IBlogService
    {
        private readonly IGenericDAL<Blog> _genericDAL;
        public BlogManager(IGenericDAL<Blog> genericDAL) : base(genericDAL)
        {
            _genericDAL = genericDAL;
        }

        public async Task<List<Blog>> GetAllSortedByPostedTimeAsync()
        {
            return await _genericDAL.GetAllAsync(x => x.PostedTime);
        }
    }
}
