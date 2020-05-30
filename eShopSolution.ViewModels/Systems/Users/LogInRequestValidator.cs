using FluentValidation;

namespace eShopSolution.ViewModels.Systems.Users
{
    public class LogInRequestValidator : AbstractValidator<LoginRequest>
    {
        public LogInRequestValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().WithMessage("UserName is required");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required")
                .MinimumLength(6).WithMessage("Password is at least 6 character");
        }
    }
}