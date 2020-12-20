using System.ComponentModel.DataAnnotations;

namespace BasicBlogFront.Models
{
    public class CategoryAddModel
    {
        [Required(ErrorMessage = "Category name is required.")]
        public string Name { get; set; }
    }
}
