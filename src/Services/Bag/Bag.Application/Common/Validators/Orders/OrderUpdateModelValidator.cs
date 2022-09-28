using Bag.Application.Dtos.Orders;

namespace Bag.Application.Common.Validators.Orders;

public class OrderUpdateModelValidator : OrderManipulateModelValidator<OrderUpdateModel>
{
    public OrderUpdateModelValidator() : base() { }
}