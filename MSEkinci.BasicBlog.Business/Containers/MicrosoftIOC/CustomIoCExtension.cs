using Microsoft.Extensions.DependencyInjection;
using MSEkinci.BasicBlog.Business.Concrete;
using MSEkinci.BasicBlog.Business.Interfaces;
using MSEkinci.BasicBlog.DataAccess.Concrete.EntityFrameworkCore.Repositories;
using MSEkinci.BasicBlog.DataAccess.Interfaces;

namespace MSEkinci.BasicBlog.Business.Containers.MicrosoftIOC
{
    public static class CustomIoCExtension
    {
        public static void AddDependencies(this IServiceCollection services)
        {
            services.AddScoped(typeof(IGenericDAL<>), typeof(EFGenericRepository<>));
            services.AddScoped(typeof(IGenericService<>), typeof(GenericManager<>));

            services.AddScoped<IAppUserDAL, EFAppUserRepository>();
            services.AddScoped<IAppUserService, AppUserManager>();

            services.AddScoped<IBlogDAL, EFBlogRepository>();
            services.AddScoped<IBlogService, BlogManager>();

            services.AddScoped<ICategoryDAL, EFCategoryRepository>();
            services.AddScoped<ICategoryService, CategoryManager>();

            services.AddScoped<ICommentDAL, EFCommentRepository>();
            services.AddScoped<ICommentService, CommentManager>();
        }
    }
}
