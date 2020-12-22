using AutoMapper;
using MSEkinci.BasicBlog.DTO.DTOs.AppUserDTOs;
using MSEkinci.BasicBlog.DTO.DTOs.BlogDTOs;
using MSEkinci.BasicBlog.DTO.DTOs.CategoryDTOs;
using MSEkinci.BasicBlog.DTO.DTOs.CommentDTOs;
using MSEkinci.BasicBlog.Entities.Concrete;
using MSEkinci.BasicBlog.WebApi.Models;

namespace MSEkinci.BasicBlog.WebApi.Mapping.AutoMapperProfile
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<BlogListDTO, Blog>();
            CreateMap<Blog, BlogListDTO>();

            CreateMap<BlogUpdateModel, Blog>();
            CreateMap<Blog, BlogUpdateModel>();

            CreateMap<BlogAddModel, Blog>();
            CreateMap<Blog, BlogAddModel>();

            CreateMap<CategoryAddDTO, Category>();
            CreateMap<Category, CategoryAddDTO>();

            CreateMap<CategoryUpdateDTO, Category>();
            CreateMap<Category, CategoryUpdateDTO>();

            CreateMap<CategoryListDTO, Category>();
            CreateMap<Category, CategoryListDTO>();

            CreateMap<AppUserLoginDTO, AppUser>();
            CreateMap<AppUser, AppUserLoginDTO>();

            CreateMap<AppUserDto, AppUser>();
            CreateMap<AppUser, AppUserDto>();

            CreateMap<Comment, CommentListDTO>();
            CreateMap<CommentListDTO, Comment>();

            CreateMap<Comment, CommentAddDTO>();
            CreateMap<CommentAddDTO, Comment>();
        }
    }
}
