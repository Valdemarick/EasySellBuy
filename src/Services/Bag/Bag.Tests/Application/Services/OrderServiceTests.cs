using AutoFixture;
using AutoMapper;
using Bag.Application.Common.Interfaces.Repositories;
using Bag.Application.Common.Interfaces.Services;
using Bag.Application.Common.Redis;
using Bag.Application.Dtos.Customers;
using Bag.Application.Services;
using Bag.Domain.Entities;
using FluentValidation;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;

namespace Bag.Tests.Application.Services;

public class OrderServiceTests
{
    private readonly Mock<IRepositoryManager> _repoManagerMock;
    private readonly Mock<ICustomerRepository> _customerRepoMock;
    private readonly Mock<IDistributedCacheService> _distributedCacheServiceMock;
    private readonly Mock<IValidator<CustomerCreateModel>> _createValidatorMock;
    private readonly Mock<IValidator<CustomerUpdateModel>> _updateValidatorMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<ILogger<OrderService>> _loggerMock;
    private readonly Mock<IOptions<RedisOptions>> _redisOptionsMock;
    private readonly IFixture _fixture;
    private readonly IOrderService _orderService;

    public OrderServiceTests()
    {
        _repoManagerMock = new();
        _customerRepoMock = new();
        _distributedCacheServiceMock = new();
        _createValidatorMock = new();
        _updateValidatorMock = new();
        _mapperMock = new();
        _loggerMock = new();
        _redisOptionsMock = new();
        _fixture = new Fixture();

        _redisOptionsMock.Object.Value.Port = 1234;
        _redisOptionsMock.Object.Value.Host = "localhost";

        _orderService = new OrderService(
            _repoManagerMock.Object,
            _mapperMock.Object,
            _distributedCacheServiceMock.Object,
            _loggerMock!.Object,
            _redisOptionsMock.Object);

        _repoManagerMock
            .Setup(r => r.CustomerRepository)
            .Returns(_customerRepoMock.Object);

        _repoManagerMock
            .Setup(r => r.SaveChangesAsync(new CancellationToken()));
    }

    [Fact]
    public async void GetAllAsync_WhenThereAreSomeCustomer_ShouldReturnListOfCustomer()
    {
        //Arrange
        const int listLength = 5;

        var customers = _fixture.CreateMany<Customer>(listLength);

        var customerDtos = _fixture.CreateMany<CustomerReadModel>(listLength);

        _customerRepoMock
            .Setup(r => r.GetAsync(default))
            .ReturnsAsync(customers);

        _mapperMock
            .Setup(m => m.Map<IEnumerable<CustomerReadModel>>(customers))
            .Returns(customerDtos);

        //Act
        var result = await _orderService.GetAsync();

        //Assert
        Assert.NotNull(result);
    }
}