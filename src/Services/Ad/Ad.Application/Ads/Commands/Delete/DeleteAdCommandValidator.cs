using FluentValidation;

namespace Ad.Application.Ads.Commands.Delete;

public class DeleteAdCommandValidator : AbstractValidator<DeleteAdCommand>
{
    public DeleteAdCommandValidator()
    {
        RuleFor(m => m.Id).NotEmpty();
    }
}