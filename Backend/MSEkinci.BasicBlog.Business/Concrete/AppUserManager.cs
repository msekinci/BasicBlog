using MSEkinci.BasicBlog.Business.Interfaces;
using MSEkinci.BasicBlog.DataAccess.Interfaces;
using MSEkinci.BasicBlog.DTO.DTOs.AppUserDTOs;
using MSEkinci.BasicBlog.Entities.Concrete;
using System.Threading.Tasks;

namespace MSEkinci.BasicBlog.Business.Concrete
{
    public class AppUserManager : GenericManager<AppUser>, IAppUserService
    {
        private readonly IGenericDAL<AppUser> _genericDAL;
        public AppUserManager(IGenericDAL<AppUser> genericDAL) : base(genericDAL)
        {
            _genericDAL = genericDAL;
        }

        public async Task<AppUser> CheckUser(AppUserLoginDTO appUserLoginDTO)
        {
            return await _genericDAL.GetAsync(x =>
            x.UserName == appUserLoginDTO.UserName &&
            x.Password == appUserLoginDTO.Password);
        }
    }
}
