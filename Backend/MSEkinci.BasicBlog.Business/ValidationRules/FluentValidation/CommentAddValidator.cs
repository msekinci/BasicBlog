using FluentValidation;
using MSEkinci.BasicBlog.DTO.DTOs.CommentDTOs;

namespace MSEkinci.BasicBlog.Business.ValidationRules.FluentValidation
{
    public class CommentAddValidator : AbstractValidator<CommentAddDTO>
    {
        public CommentAddValidator()
        {
            RuleFor(x => x.AuthorName).NotEmpty().WithMessage("Author name is required");
            RuleFor(x => x.AuthorEmail).NotEmpty().WithMessage("Author mail is required");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required");

        }
    }
}
