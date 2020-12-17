using MSEkinci.BasicBlog.Entities.Interfaces;
using System;
using System.Collections.Generic;

namespace MSEkinci.BasicBlog.Entities.Concrete
{
    public class Blog : IEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public DateTime PostedTime { get; set; } = DateTime.Now;

        public AppUser AppUser { get; set; }
        public int AppUserId { get; set; }

        public List<Comment> Comments { get; set; }
        public List<CategoryBlog> CategoryBlogs { get; set; }
    }
}
