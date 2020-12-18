using MSEkinci.BasicBlog.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace MSEkinci.BasicBlog.DTO.DTOs.CategoryDTOs
{
    public class CategoryWithBlogCount
    {
        public int BlogsCount { get; set; }
        public Category Category { get; set; }
    }
}
