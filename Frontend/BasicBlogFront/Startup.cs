using BasicBlogFront.ApiServices.Concrete;
using BasicBlogFront.ApiServices.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BasicBlogFront
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpContextAccessor(); //Bir class ile http context'e eriþebilmek için
            services.AddSession(); //Tokený tutabilmek için Session'ý aktif ediyoruz
            services.AddHttpClient<IBlogApiService, BlogApiManager>();
            services.AddHttpClient<ICategoryApiService, CategoryApiManager>();
            services.AddHttpClient<IImageApiService, ImageApiManager>();
            services.AddHttpClient<IAuthApiService, AuthApiManager>();
            services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();
            else app.UseExceptionHandler("/Error");

            app.UseSession(); //Tokený tutabilmek için Session'ý aktif ediyoruz
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
