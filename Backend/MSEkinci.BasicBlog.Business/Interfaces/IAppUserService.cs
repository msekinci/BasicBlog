using MSEkinci.BasicBlog.DTO.DTOs.AppUserDTOs;
using MSEkinci.BasicBlog.Entities.Concrete;
using System.Threading.Tasks;

namespace MSEkinci.BasicBlog.Business.Interfaces
{
    public interface IAppUserService : IGenericService<AppUser>
    {
        Task<AppUser> CheckUserAsync(AppUserLoginDTO appUserLoginDTO);
        Task<AppUser> FindByNameAsync(string username);
    }
}
