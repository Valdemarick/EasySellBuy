using Bag.Application.Common.Interfaces.Repositories;
using Bag.Persistence.Contexts;
using Bag.Persistence.Repositories;

namespace Bag.Tests.Persistence.Repositories;

public class OrderRepositoryTests
{
    private readonly ApplicationDbContext _context;
    private readonly IOrderRepository _orderRepositoryMock;

    //public OrderRepositoryTests()
    //{
    //    _context = new();
    //    _orderRepositoryMock = new OrderRepository(_context);
    //}

    [Fact]
    public async Task GetAsync_WhenThereAreExistingOrders_ShouldReturnListOfOrders()
    {
        //Arrange

    }
}