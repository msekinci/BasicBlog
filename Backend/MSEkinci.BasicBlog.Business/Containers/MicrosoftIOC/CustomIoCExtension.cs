using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using MSEkinci.BasicBlog.Business.Concrete;
using MSEkinci.BasicBlog.Business.Interfaces;
using MSEkinci.BasicBlog.Business.Tools.FacadeTool;
using MSEkinci.BasicBlog.Business.Tools.JWTTool;
using MSEkinci.BasicBlog.Business.Tools.LogTool;
using MSEkinci.BasicBlog.Business.ValidationRules.FluentValidation;
using MSEkinci.BasicBlog.DataAccess.Concrete.EntityFrameworkCore.Context;
using MSEkinci.BasicBlog.DataAccess.Concrete.EntityFrameworkCore.Repositories;
using MSEkinci.BasicBlog.DataAccess.Interfaces;
using MSEkinci.BasicBlog.DTO.DTOs.AppUserDTOs;
using MSEkinci.BasicBlog.DTO.DTOs.CategoryBlogDTOs;
using MSEkinci.BasicBlog.DTO.DTOs.CategoryDTOs;
using MSEkinci.BasicBlog.DTO.DTOs.CommentDTOs;

namespace MSEkinci.BasicBlog.Business.Containers.MicrosoftIOC
{
    public static class CustomIoCExtension
    {
        public static void AddDependencies(this IServiceCollection services)
        {
            services.AddDbContext<BlogContext>();

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

            services.AddScoped<IJwtService, JwtManager>();

            services.AddScoped<IFacade, Facade>();

            services.AddTransient<IValidator<AppUserLoginDTO>, AppUserLoginValidator>();
            services.AddTransient<IValidator<CategoryAddDTO>, CategoryAddValidator>();
            services.AddTransient<IValidator<CategoryBlogDTO>, CategoryBlogValidator>();
            services.AddTransient<IValidator<CategoryUpdateDTO>, CategoryUpdateValidator>();
            services.AddTransient<IValidator<CommentAddDTO>, CommentAddValidator>();

            services.AddScoped<ICustomLogger, NLogAdapter>();
        }
    }
}
