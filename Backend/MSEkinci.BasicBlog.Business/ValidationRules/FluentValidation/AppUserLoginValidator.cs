using FluentValidation;
using MSEkinci.BasicBlog.DTO.DTOs.AppUserDTOs;

namespace MSEkinci.BasicBlog.Business.ValidationRules.FluentValidation
{
    public class AppUserLoginValidator : AbstractValidator<AppUserLoginDTO>
    {
        public AppUserLoginValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().WithMessage("Username cannot be null");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password cannot be null");
        }
    }
}
