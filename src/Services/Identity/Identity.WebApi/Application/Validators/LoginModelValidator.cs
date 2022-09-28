using FluentValidation;
using Identity.WebApi.Application.Models;

namespace Identity.WebApi.Application.Validators;

public class LoginModelValidator : AbstractValidator<LoginModel>
{
    public LoginModelValidator()
    {
        RuleFor(m => m.UserName).NotNull().NotEmpty();

        RuleFor(m => m.Password).NotNull().NotEmpty().MinimumLength(8).Must(IsValidPassword);
    }

    private bool IsValidPassword(string password) => password.Any(char.IsLetter) && password.Any(char.IsDigit);
}