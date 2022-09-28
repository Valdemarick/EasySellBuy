using Bag.Application.Dtos.Sellers;
using FluentValidation;

namespace Bag.Application.Common.Validators.Sellers;

public class SellerManipulateModelValidator<TModel> : AbstractValidator<TModel>
    where TModel : SellerManipulateModel
{
    public SellerManipulateModelValidator()
    {
        RuleFor(m => m.UserName)
            .NotNull()
            .NotEmpty()
            .MaximumLength(30);

        RuleFor(m => m.PhoneNumber)
            .NotNull()
            .NotEmpty()
            .MaximumLength(50);
    }
}