using AutoMapper;
using Bag.Application.Common.Mappings;

namespace Bag.Tests.Application.Mapping;

public class CustomerProfileTests
{
    [Fact]
    public void TestCustomerProfile()
    {
        var mapperConfig = new MapperConfiguration(config =>
        {
            config.AddProfile(new CustomerProfile());
        });

        var mapper = new Mapper(mapperConfig);

        mapper.ConfigurationProvider.AssertConfigurationIsValid();
    }
}