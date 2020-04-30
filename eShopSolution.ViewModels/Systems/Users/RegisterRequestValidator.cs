using FluentValidation;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace eShopSolution.ViewModels.Systems.Users
{
    public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
    {
        public RegisterRequestValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("First Name is required");
            RuleFor(x => x.LastName).NotEmpty().WithMessage("Last Name is required");
            RuleFor(x => x.Dob).GreaterThan(DateTime.Now.AddYears(-100)).WithMessage("Brithday can not greater than 100 years");
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required").Matches(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$").WithMessage("Email is not match");
            RuleFor(x => x).Custom((request, context) =>
            {
                if (request.Password != request.ConfirmPassword)
                {
                    context.AddFailure("ConfirmPassword not match Password");
                }
            });
        }
    }
}