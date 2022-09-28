using Bag.Application.Common.Interfaces.Repositories;
using Bag.Domain.Entities;
using Bag.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Bag.Persistence.Repositories;

public sealed class OrderRepository : BaseRepository<Order>, IOrderRepository
{
    public OrderRepository(ApplicationDbContext context) : base(context) { }

    public override async Task<IEnumerable<Order>> GetAsync(CancellationToken token)
    {
        return await dbSet
            .Include(m => m.Seller)
            .Include(m => m.Customer)
            .AsNoTracking()
            .ToListAsync(token);
    }

    public override async Task<Order?> GetAsync(int id, CancellationToken token)
    {
        return await dbSet
            .Include(m => m.Seller)
            .Include(m => m.Customer)
            .AsNoTracking()
            .SingleOrDefaultAsync(m => m.Id == id, token);
    }
}