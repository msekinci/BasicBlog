using BasicBlogFront.Models;
using System.Threading.Tasks;

namespace BasicBlogFront.ApiServices.Interfaces
{
    public interface IAuthApiService
    {
        Task<bool> SignIn(AppUserLoginModel model);
    }
}
