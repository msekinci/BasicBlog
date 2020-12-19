using BasicBlogFront.ApiServices.Interfaces;
using BasicBlogFront.Enums;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Threading.Tasks;

namespace BasicBlogFront.TagHelpers
{
    [HtmlTargetElement("getblogimage")]
    public class ImageTagHelper : TagHelper
    {
        private readonly IImageApiService _imageApiService;
        public int Id { get; set; }
        public BlogImageType BlogImageType { get; set; }

        public ImageTagHelper(IImageApiService imageApiService)
        {
            _imageApiService = imageApiService;
        }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            string html = string.Empty;
            var blob = await _imageApiService.GetBlogImageByIdAsync(Id);

            if (BlogImageType == BlogImageType.BlogHome)
            {
                html = $"<img class='card-img-top' src='{blob}'> ";
            }
            else
            {
                html = $"<img class='img-fluid rounded' src='{blob}'> ";
            }
            output.Content.SetHtmlContent(html);
        }
    }
}
