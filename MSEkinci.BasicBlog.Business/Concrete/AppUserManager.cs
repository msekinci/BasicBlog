using MSEkinci.BasicBlog.Business.Interfaces;
using MSEkinci.BasicBlog.DataAccess.Interfaces;
using MSEkinci.BasicBlog.Entities.Concrete;

namespace MSEkinci.BasicBlog.Business.Concrete
{
    public class AppUserManager : GenericManager<AppUser>, IAppUserService
    {
        private readonly IGenericDAL<AppUser> _genericDAL;
        public AppUserManager(IGenericDAL<AppUser> genericDAL) : base(genericDAL)
        {
            _genericDAL = genericDAL;
        }
    }
}
