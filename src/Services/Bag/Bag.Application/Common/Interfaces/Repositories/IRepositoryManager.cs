namespace Bag.Application.Common.Interfaces.Repositories;

public interface IRepositoryManager
{
    IOrderRepository OrderRepository { get; }
    ISellerRepository SellerRepository { get; }
    ICustomerRepository CustomerRepository { get; }
    
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}