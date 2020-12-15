using Microsoft.Extensions.DependencyInjection;
using MSEkinci.BasicBlog.Business.Concrete;
using MSEkinci.BasicBlog.Business.Interfaces;
using MSESoftware.BasicBlog.DataAccess.Concrete.EntityFrameworkCore.Repositories;
using MSESoftware.BasicBlog.DataAccess.Interfaces;

namespace MSEkinci.BasicBlog.Business.Containers.MicrosoftIOC
{
    public static class CustomIoCExtension
    {
        public static void AddDependencies(this IServiceCollection services)
        {
            services.AddScoped(typeof(IGenericDAL<>), typeof(EFGenericRepository<>));
            services.AddScoped(typeof(IGenericService<>), typeof(GenericManager<>));
        }
    }
}
