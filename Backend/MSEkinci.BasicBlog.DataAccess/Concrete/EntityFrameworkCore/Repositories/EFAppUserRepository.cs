using MSEkinci.BasicBlog.DataAccess.Concrete.EntityFrameworkCore.Context;
using MSEkinci.BasicBlog.DataAccess.Interfaces;
using MSEkinci.BasicBlog.Entities.Concrete;

namespace MSEkinci.BasicBlog.DataAccess.Concrete.EntityFrameworkCore.Repositories
{
    public class EFAppUserRepository : EFGenericRepository<AppUser>, IAppUserDAL
    {
        public EFAppUserRepository(BlogContext context) : base(context)
        {

        }
    }
}
