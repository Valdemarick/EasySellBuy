using AutoMapper;
using Bag.Application.Common.Interfaces.Repositories;
using Bag.Application.Common.Interfaces.Services;
using Bag.Application.Common.Redis;
using Bag.Application.Dtos.Orders;
using Bag.Domain.Entities;
using Exceptions.CustomExceptions;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RedLockNet.SERedis;
using RedLockNet.SERedis.Configuration;
using System.Net;

namespace Bag.Application.Services;

public class OrderService : BaseService, IOrderService
{
    private readonly IDistributedCacheService _distributedCacheService;
    private readonly ILogger<OrderService> _logger;
    private readonly RedisOptions _redisOptions;
    private readonly List<RedLockEndPoint> _endPoints;

    public OrderService(
        IRepositoryManager repoManager,
        IMapper mapper,
        IDistributedCacheService distributedCacheService,
        ILogger<OrderService> logger,
        IOptions<RedisOptions> redisOptions) : base(repoManager, mapper)
    {
        _distributedCacheService = distributedCacheService ?? throw new ArgumentNullException(nameof(distributedCacheService));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _redisOptions = redisOptions.Value ?? throw new ArgumentNullException(nameof(redisOptions));

        _endPoints = new List<RedLockEndPoint>()
        {
            new DnsEndPoint(_redisOptions.Host, _redisOptions.Port)
        };
    }

    public async Task<IEnumerable<OrderReadModel>> GetAsync(CancellationToken cancellationToken)
    {
        var orders = await repoManager.OrderRepository.GetAsync(cancellationToken);

        return mapper.Map<IEnumerable<OrderReadModel>>(orders);
    }

    public async Task<OrderReadModel?> GetAsync(int id, CancellationToken cancellationToken)
    {
        var order = await repoManager.OrderRepository.GetAsync(id, cancellationToken);

        return mapper.Map<OrderReadModel>(order);
    }

    public async Task<IEnumerable<OrderReadModel>> GetRandomlyAsync(string key, CancellationToken cancellationToken)
    {
        var cache = await _distributedCacheService.GetAsync<IEnumerable<OrderReadModel>>(key, cancellationToken);
        if (cache != null)
        {
            return cache;
        }

        var allOrders = await GetAsync(cancellationToken);
        var randomOrders = allOrders
            .OrderBy(r => new Random().Next())
            .Take(3);

        await _distributedCacheService.SetAsync(key, randomOrders, new DistributedCacheEntryOptions()
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30)
        }, cancellationToken);

        return randomOrders;
    }

    public Task GetLock()
    {
        var redlockFactory = RedLockFactory.Create(_endPoints);

        TimeSpan expiry = TimeSpan.FromSeconds(30);
        var wait = TimeSpan.FromSeconds(25);
        var retry = TimeSpan.FromSeconds(1);

        using (var redLock = redlockFactory.CreateLock(null!, expiry, wait, retry))
        {
            if (redLock.IsAcquired)
            {
                _logger.LogInformation($"The lock acquired at {DateTimeOffset.Now.ToString("dd-MM-yyyy HH:mm:ss")}");

                // do some task
                Thread.Sleep(10000);

                _logger.LogInformation($"The lock has been released");
            }
            else
            {
                Console.WriteLine($"Failed to get the lock");
            }
        }

        return Task.CompletedTask;
    }

    public async Task<OrderReadModel> CreateAsync(OrderCreateModel model, CancellationToken cancellationToken)
    {
        if (model is null)
        {
            throw new ArgumentNullException(nameof(model));
        }

        await VerifyCustomerAndSellerExistAsync(model, cancellationToken);

        var order = mapper.Map<Order>(model);

        var createdEntity = await repoManager.OrderRepository.CreateAsync(order, cancellationToken);
        await repoManager.SaveChangesAsync(cancellationToken);

        return mapper.Map<OrderReadModel>(createdEntity);
    }

    public async Task UpdateAsync(int id, OrderUpdateModel model, CancellationToken cancellationToken)
    {
        if (model is null)
        {
            throw new ArgumentNullException(nameof(model));
        }

        var existingOrder = await repoManager.OrderRepository.FindAsync(
            m => m.Id == id, cancellationToken);
        if (existingOrder is null)
        {
            throw new NotFoundException($"The order with id:{id} doesn't exist in the database");
        }

        await VerifyCustomerAndSellerExistAsync(model, cancellationToken);

        mapper.Map(model, existingOrder);

        await repoManager.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken)
    {
        var existingOrder = await repoManager.OrderRepository.FindAsync(m => m.Id == id, cancellationToken);
        if (existingOrder is null)
        {
            throw new NotFoundException($"The order with id:{id} doesn't exist in the database");
        }

        repoManager.OrderRepository.Delete(existingOrder);
        await repoManager.SaveChangesAsync(cancellationToken);
    }

    private async Task VerifyCustomerAndSellerExistAsync(OrderManipulateModel model, CancellationToken cancellationToken)
    {
        var seller = await repoManager.SellerRepository.FindAsync(m => m.Id == model.SellerId);
        var customer = await repoManager.CustomerRepository.FindAsync(m => m.Id == model.CustomerId);

        if (seller is null)
        {
            throw new NotFoundException($"The seller with id: {model.SellerId} doesn't exist in the database");
        }
        else if (customer is null)
        {
            throw new NotFoundException($"The customer with id: {model.CustomerId} doesn't exist in the database");
        }
    }
}