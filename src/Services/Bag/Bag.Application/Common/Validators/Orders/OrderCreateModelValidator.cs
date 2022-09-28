using Bag.Application.Dtos.Orders;

namespace Bag.Application.Common.Validators.Orders;

public class OrderCreateModelValidator : OrderManipulateModelValidator<OrderCreateModel>
{
    public OrderCreateModelValidator() : base() { }
}