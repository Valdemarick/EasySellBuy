using AutoFixture;
using AutoMapper;
using Bag.Application.Common.Interfaces.Repositories;
using Bag.Application.Common.Interfaces.Services;
using Bag.Application.Dtos.Customers;
using Bag.Application.Services;
using Bag.Domain.Entities;
using Exceptions.CustomExceptions;
using Moq;

namespace Bag.Tests.Application.Services;

public class CustomerServiceTests
{
    private readonly Mock<IRepositoryManager> _repoManagerMock;
    private readonly Mock<ICustomerRepository> _customerRepoMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly IFixture _fixture;
    private readonly ICustomerService _customerService;

    public CustomerServiceTests()
    {
        _repoManagerMock = new();
        _customerRepoMock = new();
        _mapperMock = new();
        _fixture = new Fixture();

        _customerService = new CustomerService(
            _repoManagerMock.Object,
            _mapperMock.Object);

        _repoManagerMock
            .Setup(r => r.CustomerRepository)
            .Returns(_customerRepoMock.Object);

        _repoManagerMock
            .Setup(r => r.SaveChangesAsync(new CancellationToken()));

        _fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
        _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
    }

    [Fact]
    public async Task GetAsync_WhenThereAreSomeCustomer_ShouldReturnListOfCustomer()
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
        var result = await _customerService.GetAsync();

        //Assert
        Assert.NotNull(result);
        Assert.Equal(customerDtos, result);
    }

    [Fact]
    public async Task GetAsync_WhenThereAreNotAnyCustomers_ShouldReturnEmptyList()
    {
        const int listLength = 0;

        var customers = _fixture.CreateMany<Customer>(listLength);

        var customerDtos = _fixture.CreateMany<CustomerReadModel>(listLength);

        _customerRepoMock
            .Setup(r => r.GetAsync(default))
            .ReturnsAsync(customers);

        _mapperMock
            .Setup(m => m.Map<IEnumerable<CustomerReadModel>>(customers))
            .Returns(customerDtos);

        //Act
        var result = await _customerService.GetAsync();

        //Assert
        Assert.Empty(result);
    }

    [Fact]
    public async Task GetAsync_WhenThereIsCustomerWithValidId_ShouldReturnThisCustomer()
    {
        //Arrange
        const int id = 1;

        var customer = _fixture
            .Build<Customer>()
            .With(m => m.Id, id)
            .Create();

        var customerDto = _fixture
            .Build<CustomerReadModel>()
            .With(m => m.Id, id)
            .Create();

        _customerRepoMock
            .Setup(r => r.GetAsync(id, default))
            .ReturnsAsync(customer);

        _mapperMock
            .Setup(m => m.Map<CustomerReadModel>(customer))
            .Returns(customerDto);

        //Act
        var result = await _customerService.GetAsync(id);

        //Assert
        Assert.NotNull(result);
        Assert.Equal(customerDto, result);
    }

    [Fact]
    public async Task GetAsync_WhenThereIsNotCustomerWithValidId_ShouldReturnNull()
    {
        //Arrange
        const int id = 1;

        var customer = _fixture
            .Build<Customer>()
            .With(m => m.Id, id)
            .Create();

        var customerDto = _fixture
            .Build<CustomerReadModel>()
            .With(m => m.Id, id)
            .Create();

        _customerRepoMock
            .Setup(r => r.GetAsync(id, default))
            .ReturnsAsync(customer);

        _mapperMock
            .Setup(m => m.Map<CustomerReadModel>(customer))
            .Returns(customerDto);

        //Act
        var result = await _customerService.GetAsync(5);

        //Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task CreateAsync_WhenValidModelIsPassed_ShouldReturnCreatedUser_VerifyCreateAndSaveMethods()
    {
        //Arrange
        var customer = _fixture.Create<Customer>();

        var customerCreateDto = _fixture.Create<CustomerCreateModel>();

        var customerDto = _fixture.Create<CustomerReadModel>();

        _customerRepoMock
            .Setup(r => r.CreateAsync(customer, default))
            .ReturnsAsync(customer);

        _mapperMock
            .Setup(m => m.Map<Customer>(customerCreateDto))
            .Returns(customer);

        _mapperMock
            .Setup(m => m.Map<CustomerReadModel>(customer))
            .Returns(customerDto);

        //Act
        var result = await _customerService.CreateAsync(customerCreateDto);

        //Assert
        Assert.NotNull(result);
        Assert.Equal(customerDto, result);

        _repoManagerMock.Verify(r => r.SaveChangesAsync(default), Times.Once);
        _customerRepoMock.Verify(r => r.CreateAsync(customer, default), Times.Once);
    }

    [Fact]
    public async Task CreateAsync_WhenNullIsPassed_ShouldThrowArgumentNullException()
    {
        //Act
        var result = async () => await _customerService.CreateAsync(null!);

        //Assert
        await Assert.ThrowsAsync<ArgumentNullException>(result);
    }

    [Fact]
    public async Task UpdateAsync_WhenValidModelAndIdArePassed_VerifyMapAndSaveMethods()
    {
        //Arrange
        const int id = 1;

        var customerUpdateModel = _fixture.Create<CustomerUpdateModel>();

        var customer = _fixture
            .Build<Customer>()
            .With(m => m.Id, id)
            .Create();

        _customerRepoMock
            .Setup(r => r.FindAsync(m => m.Id == id, default))
            .ReturnsAsync(customer);

        //Act
        await _customerService.UpdateAsync(id, customerUpdateModel);

        //Assert
        _mapperMock.Verify(m => m.Map(customerUpdateModel, customer), Times.Once);
        _repoManagerMock.Verify(r => r.SaveChangesAsync(default), Times.Once);
    }

    [Fact]
    public async Task UpdateAsync_WhenValidModelAndInvalidIdArePasses_ShouldThrowNotFoundException()
    {
        //Arrange
        const int id = 1;

        var customerUpdateModel = _fixture.Create<CustomerUpdateModel>();

        var customer = _fixture.Create<Customer>();

        _customerRepoMock
            .Setup(r => r.FindAsync(m => m.Id == id, default))
            .ReturnsAsync(customer);

        //Act
        var result = async () => await _customerService.UpdateAsync(0, customerUpdateModel);

        //Assert
        await Assert.ThrowsAsync<NotFoundException>(result);
    }

    [Fact]
    public async Task UpdateAsync_WhenNullIsPassed_ShouldThrowArgumentNullException()
    {
        //Act
        var result = async () => await _customerService.UpdateAsync(0, null!);

        //Assert
        await Assert.ThrowsAsync<ArgumentNullException>(result);
    }

    [Fact]
    public async Task DeleteAsync_WhenValidIdIsPassed_VerifyDeleteAndSaveMethods()
    {
        //Arrange
        const int id = 1;

        var customer = _fixture
            .Build<Customer>()
            .With(m => m.Id, id)
            .Create();

        _customerRepoMock
            .Setup(r => r.FindAsync(m => m.Id == id, default))
            .ReturnsAsync(customer);

        //Act
        await _customerService.DeleteAsync(id);

        //Assert
        _customerRepoMock.Verify(r => r.Delete(customer), Times.Once);
        _repoManagerMock.Verify(r => r.SaveChangesAsync(default), Times.Once);
    }

    [Fact]
    public async Task DeleteAsync_WhenInvalidIdIsPassed_ShouldThrowNotFoundException()
    {
        //Act
        var result = async () => await _customerService.DeleteAsync(0);

        //Assert
        await Assert.ThrowsAsync<NotFoundException>(result);
    }
}