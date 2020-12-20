using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace BasicBlogFront.Models
{
    public class BlogAddModel
    {
        public int AppUserId { get; set; }
        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Short Description is required")]
        [Display(Name = "Short Description")]
        public string ShortDescription { get; set; }
        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }
        public IFormFile Image { get; set; }
    }
}
