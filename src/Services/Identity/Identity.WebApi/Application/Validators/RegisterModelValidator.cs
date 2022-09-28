using FluentValidation;
using Identity.WebApi.Application.Models;

namespace Identity.WebApi.Application.Validators;

public class RegisterModelValidator : AbstractValidator<RegisterModel>
{
    public RegisterModelValidator()
    {
        RuleFor(m => m.UserName).NotNull().NotEmpty();

        RuleFor(m => m.Password).NotNull().NotEmpty().Must(IsValidPassword).MinimumLength(8);

        RuleFor(m => m.ConfirmPassword).NotNull().NotEmpty().Must(IsValidPassword).Equal(m => m.Password);

        RuleFor(m => m.Email).NotNull().NotEmpty().EmailAddress();

        RuleFor(m => m.FirstName).NotNull().NotEmpty().Must(IsValidNameAndSurname).Length(3, 100);

        RuleFor(m => m.LastName).NotNull().NotEmpty().Must(IsValidNameAndSurname).Length(5, 100);

        RuleFor(m => m.DateOfBirth).NotEmpty().Must(CheckDateOfBirth);
    }

    private bool IsValidNameAndSurname(string name) => name.All(char.IsLetter);

    private bool IsValidPassword(string password) => password.Any(char.IsLetter) && password.Any(char.IsDigit);

    private bool CheckDateOfBirth(DateOnly dateOfBirth)
    {
        var sub = ((DateTime.Now - new DateTime(dateOfBirth.Year, dateOfBirth.Month, dateOfBirth.Day)).Days) / 365;

        return sub >= 14 ? true : false;
    }
}