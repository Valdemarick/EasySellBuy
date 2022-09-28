using AutoMapper;
using Bag.Application.Common.Mappings;

namespace Bag.Tests.Application.Mapping;

public class SellerProfileTests
{
    [Fact]
    public void ValidateSellerMappingProfileTests()
    {
        var mapperConfig = new MapperConfiguration(config =>
        {
            config.AddProfile(new SellerProfile());
        });

        var mapper = new Mapper(mapperConfig);

        mapper.ConfigurationProvider.AssertConfigurationIsValid();
    }
}