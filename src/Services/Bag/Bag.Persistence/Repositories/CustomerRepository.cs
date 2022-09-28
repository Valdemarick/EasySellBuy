using Bag.Application.Common.Interfaces.Repositories;
using Bag.Domain.Entities;
using Bag.Persistence.Contexts;

namespace Bag.Persistence.Repositories;

public sealed class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
{
    public CustomerRepository(ApplicationDbContext context) : base(context) { }
}