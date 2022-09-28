using Bag.Application.Dtos.Customers;

namespace Bag.Application.Common.Interfaces.Services;

public interface ICustomerService
{
    Task<IEnumerable<CustomerReadModel>> GetAsync(CancellationToken cancellationToken = default);
    Task<CustomerReadModel?> GetAsync(int id, CancellationToken cancellationToken = default);
    Task<CustomerReadModel> CreateAsync(CustomerCreateModel model, CancellationToken cancellationToken = default);
    Task UpdateAsync(int id, CustomerUpdateModel model, CancellationToken cancellationToken = default);
    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
}