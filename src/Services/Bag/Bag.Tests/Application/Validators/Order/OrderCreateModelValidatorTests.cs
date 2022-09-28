using AutoFixture;
using AutoFixture.Xunit2;
using Bag.Application.Common.Validators.Orders;
using Bag.Application.Dtos.Orders;

namespace Bag.Tests.Application.Validators.Order;

public class OrderCreateModelValidatorTests
{
    private readonly OrderCreateModelValidator _validator;

    public OrderCreateModelValidatorTests()
    {
        _validator = new();
    }

    private OrderCreateModel BuildValidModel()
    {
        return new Fixture()
            .Build<OrderCreateModel>()
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
    public void Validate_WhenTotalAmountIsInvalid_ShouldReturnFalse(decimal invalidTotalAmount)
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
