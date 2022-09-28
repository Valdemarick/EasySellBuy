using AutoMapper;
using Bag.Application.Common.Mappings;

namespace Bag.Tests.Application.Mapping;

public class OrderProfileTests
{
    [Fact]
    public void ValidateOrderMappingProfileTests()
    {
        var mapperConfig = new MapperConfiguration(config =>
        {
            config.AddProfile(new OrderProfile());
        });

        var mapper = new Mapper(mapperConfig);

        mapper.ConfigurationProvider.AssertConfigurationIsValid();
    }
}