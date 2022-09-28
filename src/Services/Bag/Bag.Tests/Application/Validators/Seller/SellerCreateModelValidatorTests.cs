using AutoFixture;
using Bag.Application.Common.Validators.Sellers;
using Bag.Application.Dtos.Sellers;

namespace Bag.Tests.Application.Validators.Seller;

public class SellerCreateModelValidatorTests
{
    private readonly SellerCreateModelValidator _validator;

    public SellerCreateModelValidatorTests()
    {
        _validator = new();
    }

    private SellerCreateModel BuildValidModel()
    {
        return new Fixture()
            .Build<SellerCreateModel>()
            .With(m => m.UserName, "Valdemar")
            .With(m => m.PhoneNumber, "+375294231221")
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
    [InlineData("qweqweqwhbhpfbfhasdbfasdbhfsdahlbfsdlabflhbsdafhlsdabfsdljahfbsdalhjfbsdahjlbfjhl")]
    public void Validate_WhenUserNameIsInvalid_ShouldReturnFalse(string invalidUserName)
    {
        var invalidModel = BuildValidModel();
        invalidModel.UserName = invalidUserName;

        Assert.False(_validator.Validate(invalidModel).IsValid);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("+32139129387129745329587328534987598763415781348759814379857439875981347598473198")]
    public void Validate_WhenPhoneNumberIsInvalid_ShouldReturnFalse(string invalidPhoneNumber)
    {
        var invalidModel = BuildValidModel();
        invalidModel.PhoneNumber = invalidPhoneNumber;

        Assert.False(_validator.Validate(invalidModel).IsValid);
    }
}