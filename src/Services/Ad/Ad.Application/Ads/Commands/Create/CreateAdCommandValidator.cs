using FluentValidation;

namespace Ad.Application.Ads.Commands.Create;

public class CreateAdCommandValidator : AbstractValidator<CreateAdCommand>
{
    public CreateAdCommandValidator()
    {
        RuleFor(m => m.Category).IsInEnum();

        RuleFor(m => m.State).IsInEnum();

        RuleFor(m => m.AddressId).NotEmpty();

        RuleFor(m => m.UserInfoId).NotEmpty();

        RuleFor(m => m.Price).GreaterThan(0).NotEmpty();

        RuleFor(m => m.Description).NotNull().NotEmpty();

        RuleFor(m => m.Title).MaximumLength(50).NotNull().NotEmpty();
    }
}