using AutoFixture;
using Bag.Application.Common.Validators.Customers;
using Bag.Application.Dtos.Customers;

namespace Bag.Tests.Application.Validators.Customer;

public class CustomerUpdateModelValidatorTests
{
    private readonly CustomerUpdateModelValidator _validator;

    public CustomerUpdateModelValidatorTests()
    {
        _validator = new();
    }

    private CustomerUpdateModel BuildValidModel()
    {
        return new Fixture()
            .Build<CustomerUpdateModel>()
            .With(m => m.UserName, "Valdemar")
            .With(m => m.PhoneNumber, "+375441233123")
            .Create();
    }

    [Fact]
    public void Validate_WhenModelIsValid_ShouldReturnTrue()
    {
        var validModel = BuildValidModel();

        Assert.True(_validator.Validate(validModel).IsValid);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("dsadkfsdafjklsdjfjsadkfbnjkasdbfjsdfkasidfjiasdfjibdsabfjksa")]
    public void Validate_WhenUserNameIsInvalid_ShouldReturnFalse(string invalidUserName)
    {
        var invalidModel = BuildValidModel();
        invalidModel.UserName = invalidUserName;

        Assert.False(_validator.Validate(invalidModel).IsValid);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("+371239128379812637812636812683129873981269837129873981279387129837")]
    public void Validate_WhenPhoneNumberIsInvalid_ShouldReturnFalse(string invalidPhoneNumber)
    {
        var invalidModel = BuildValidModel();
        invalidModel.PhoneNumber = invalidPhoneNumber;

        Assert.False(_validator.Validate(invalidModel).IsValid);
    }
}