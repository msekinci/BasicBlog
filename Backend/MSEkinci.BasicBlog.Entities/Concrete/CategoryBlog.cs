using MSEkinci.BasicBlog.Entities.Interfaces;

namespace MSEkinci.BasicBlog.Entities.Concrete
{
    public class CategoryBlog : IEntity
    {
        public int Id { get; set; }
        public int BlogId { get; set; }
        public int CategoryId { get; set; }

        public Blog Blog { get; set; }
        public Category Category { get; set; }
    }
}
