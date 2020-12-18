using FluentValidation;
using MSEkinci.BasicBlog.DTO.DTOs.CategoryBlogDTOs;

namespace MSEkinci.BasicBlog.Business.ValidationRules.FluentValidation
{
    public class CategoryBlogValidator : AbstractValidator<CategoryBlogDTO>
    {
        public CategoryBlogValidator()
        {
            RuleFor(x => x.BlogId).InclusiveBetween(0, int.MaxValue).WithMessage("CategoryId has to be");
            RuleFor(x => x.CategoryId).InclusiveBetween(0, int.MaxValue).WithMessage("CategoryId has to be");
        }
    }
}
