using Bag.Application.Common.Interfaces.Repositories;
using Bag.Domain.Entities;
using Bag.Persistence.Contexts;

namespace Bag.Persistence.Repositories;

public sealed class SellerRepository : BaseRepository<Seller>, ISellerRepository
{
    public SellerRepository(ApplicationDbContext context) : base(context) { }
}