using Bag.Application.Dtos.Orders;
using FluentValidation;

namespace Bag.Application.Common.Validators.Orders;

public class OrderManipulateModelValidator<T> : AbstractValidator<T>
    where T : OrderManipulateModel
{
    public OrderManipulateModelValidator()
    {
        RuleFor(m => m.SellerId).NotEmpty();

        RuleFor(m => m.CustomerId).NotEmpty();

        RuleFor(m => m.TotalAmount).NotEmpty().GreaterThan(0);
    }
}
