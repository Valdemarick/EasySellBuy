using Bag.Application.Dtos.Orders;

namespace Bag.Application.Common.Interfaces.Services;

public interface IOrderService
{
    Task<IEnumerable<OrderReadModel>> GetAsync(CancellationToken cancellationToken = default);
    Task<OrderReadModel?> GetAsync(int id, CancellationToken cancellationToken = default);
    Task<IEnumerable<OrderReadModel>> GetRandomlyAsync(string key, CancellationToken cancellationToken = default);
    Task GetLock();
    Task<OrderReadModel> CreateAsync(OrderCreateModel model, CancellationToken cancellationToken = default);
    Task UpdateAsync(int id, OrderUpdateModel model, CancellationToken cancellationToken = default);
    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
}