using Bag.Application.Dtos.Customers;

namespace Bag.Application.Common.Validators.Customers;

public class CustomerUpdateModelValidator : CustomerManipulateModelValidator<CustomerUpdateModel>
{
    public CustomerUpdateModelValidator() : base() { }
}