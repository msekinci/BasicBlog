using System.Threading.Tasks;

namespace BasicBlogFront.ApiServices.Interfaces
{
    public interface IImageApiService
    {
        Task<string> GetBlogImageByIdAsync(int id);
    }
}
