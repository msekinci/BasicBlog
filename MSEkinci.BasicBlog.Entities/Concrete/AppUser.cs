using MSESoftware.BasicBlog.Entities.Concrete;
using MSESoftware.BasicBlog.Entities.Interfaces;
using System.Collections.Generic;

namespace MSESoftware.BasicBlog.Entities.Concrete
{
    public class AppUser : IEntity
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }

        public List<Blog> Blogs { get; set; }
    }
}
