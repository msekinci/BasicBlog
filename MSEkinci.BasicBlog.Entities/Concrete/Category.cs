using MSESoftware.BasicBlog.Entities.Interfaces;
using System.Collections.Generic;

namespace MSESoftware.BasicBlog.Entities.Concrete
{
    public class Category : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<CategoryBlog> CategoryBlogs { get; set; }
    }
}
