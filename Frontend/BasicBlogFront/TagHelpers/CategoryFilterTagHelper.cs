using BasicBlogFront.ApiServices.Interfaces;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Threading.Tasks;

namespace BasicBlogFront.TagHelpers
{
    [HtmlTargetElement("categoryfilter")]
    public class CategoryFilterTagHelper : TagHelper
    {
        private readonly ICategoryApiService _categoryApiService;
        public int? CategoryId { get; set; }

        public CategoryFilterTagHelper(ICategoryApiService categoryApiService)
        {
            _categoryApiService = categoryApiService;
        }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            if (CategoryId.HasValue)
            {
                var category = await _categoryApiService.GetByIdAsync(CategoryId.Value);
                string html = $"<div class='border border-info p-3 mb-2'>{category.Name} blogs are listed." +
                $"<a class='float-right' href='Home'>Remove Filter</a></div>";
                output.Content.SetHtmlContent(html);
            }
        }
    }
}
