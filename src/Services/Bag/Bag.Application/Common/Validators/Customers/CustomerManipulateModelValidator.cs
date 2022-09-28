using Bag.Application.Dtos.Customers;
using FluentValidation;

namespace Bag.Application.Common.Validators.Customers;

public class CustomerManipulateModelValidator<TModel> : AbstractValidator<TModel>
    where TModel : CustomerManipulateModel
{
    public CustomerManipulateModelValidator()
    {
        RuleFor(m => m.UserName)
            //.NotNull()
            .NotEmpty()
            .MaximumLength(30);

        RuleFor(m => m.PhoneNumber)
            .NotNull()
            .NotEmpty()
            .MaximumLength(50);
    }
}