using Bag.Application.Dtos.Customers;

namespace Bag.Application.Common.Validators.Customers;

public class CustomerCreateModelValidator : CustomerManipulateModelValidator<CustomerCreateModel>
{
    public CustomerCreateModelValidator() : base() { }
}