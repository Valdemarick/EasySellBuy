using AutoFixture;
using Bag.Application.Common.Validators.Orders;
using Bag.Application.Dtos.Orders;

namespace Bag.Tests.Application.Validators.Order;

public class OrderUpdateModelValidatorTests
{
    private readonly OrderUpdateModelValidator _validator;

    public OrderUpdateModelValidatorTests()
    {
        _validator = new();
    }

    private OrderUpdateModel BuildValidModel()
    {
        return new Fixture()
            .Build<OrderUpdateModel>()
            .With(m => m.SellerId, 1)
            .With(m => m.CustomerId, 1)
            .With(m => m.TotalAmount, 100)
            .Create();
    }

    [Fact]
    public void Validate_WhenModelIsValid_ShouldReturnTrue()
    {
        var validModel = BuildValidModel();

        Assert.True(_validator.Validate(validModel).IsValid);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Validate_WhenTotalAmoundIsInvalid_ShouldReturnFalse(decimal invalidTotalAmount)
    {
        var invalidModel = BuildValidModel();
        invalidModel.TotalAmount = invalidTotalAmount;

        Assert.False(_validator.Validate(invalidModel).IsValid);
    }

    [Fact]
    public void Validate_WhenCustomerIdIsEmpty_ShouldReturnFalse()
    {
        var invalidModel = BuildValidModel();
        invalidModel.CustomerId = 0;

        Assert.False(_validator.Validate(invalidModel).IsValid);
    }

    [Fact]
    public void Validate_WhenSellerIdIsEmpty_ShouldReturnFalse()
    {
        var invalidModel = BuildValidModel();
        invalidModel.SellerId = 0;

        Assert.False(_validator.Validate(invalidModel).IsValid);
    }
}
