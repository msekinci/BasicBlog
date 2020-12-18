using FluentValidation;
using MSEkinci.BasicBlog.DTO.DTOs.CategoryDTOs;

namespace MSEkinci.BasicBlog.Business.ValidationRules.FluentValidation
{
    class CategoryUpdateValidator : AbstractValidator<CategoryUpdateDTO>
    {
        public CategoryUpdateValidator()
        {
            RuleFor(x => x.Id).InclusiveBetween(0, int.MaxValue).WithMessage("Id has to be");
            RuleFor(x => x.Name).NotEmpty().WithMessage("Category Name cannot be empty");
        }
    }
}
