using Bag.Application.Common.Interfaces.Repositories;
using Bag.Persistence.Contexts;

namespace Bag.Persistence.Repositories;

public class RepositoryManager : IRepositoryManager
{
    private readonly ApplicationDbContext _context;

    private IOrderRepository _orderRepository;
    private ISellerRepository _sellerRepository;
    private ICustomerRepository _customerRepository;

    public RepositoryManager(ApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public IOrderRepository OrderRepository => 
        _orderRepository ??= new OrderRepository(_context);

    public ISellerRepository SellerRepository => 
        _sellerRepository ??= new SellerRepository(_context);

    public ICustomerRepository CustomerRepository => 
        _customerRepository ??= new CustomerRepository(_context);

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }
}