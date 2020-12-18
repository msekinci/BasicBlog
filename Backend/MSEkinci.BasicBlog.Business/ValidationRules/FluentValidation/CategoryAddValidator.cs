using FluentValidation;
using MSEkinci.BasicBlog.DTO.DTOs.CategoryDTOs;

namespace MSEkinci.BasicBlog.Business.ValidationRules.FluentValidation
{
    public class CategoryAddValidator : AbstractValidator<CategoryAddDTO>
    {
        public CategoryAddValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Category Name cannot be empty");
        }
    }
}
