using MSEkinci.BasicBlog.DTO.Interfaces;
using System;

namespace MSEkinci.BasicBlog.DTO.DTOs.BlogDTOs
{
    public class BlogListDTO : IDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public DateTime PostedTime { get; set; }
    }
}
