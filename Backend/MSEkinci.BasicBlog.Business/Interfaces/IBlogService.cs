﻿using MSEkinci.BasicBlog.DTO.DTOs.CategoryBlogDTOs;
using MSEkinci.BasicBlog.Entities.Concrete;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MSEkinci.BasicBlog.Business.Interfaces
{
    public interface IBlogService : IGenericService<Blog>
    {
        Task<List<Blog>> GetAllSortedByPostedTimeAsync();
        Task AddToCategoryAsync(CategoryBlogDTO categoryBlogDTO);
        Task RemoveFromCategoryAsync(CategoryBlogDTO categoryBlogDTO);
        Task<List<Blog>> GetAllByCategoryId(int categoryId);
    }
}
