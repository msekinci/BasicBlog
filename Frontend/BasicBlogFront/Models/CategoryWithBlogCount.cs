namespace BasicBlogFront.Models
{
    public class CategoryWithBlogCount
    {
        public int BlogsCount { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
}
