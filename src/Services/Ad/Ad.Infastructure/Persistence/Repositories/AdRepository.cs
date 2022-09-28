using Ad.Application.Interfaces.Repositories;
using Ad.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Ad.Infrastructure.Persistence.Repositories;

public class AdRepository : BaseRepository<AdModel>, IAdRepository
{
    public AdRepository(ApplicationDbContext context) : base(context) { }

    public override async Task<IEnumerable<AdModel>> GetAsync(CancellationToken cancellationToken)
    {
        return await context.Set<AdModel>()
            .Include(m => m.UserInfo)
            .Include(m => m.Address)
                .ThenInclude(a => a.Country)
            .Include(m => m.Address)
                .ThenInclude(a => a.City)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public override async Task<AdModel?> GetAsync(int id, CancellationToken cancellationToken)
    {
        return await context.Set<AdModel>()
            .Include(m => m.UserInfo)
            .Include(m => m.Address)
                .ThenInclude(a => a.Country)
            .Include(m => m.Address)
                .ThenInclude(a => a.City)
            .AsNoTracking()
            .SingleOrDefaultAsync(m => m.Id == id, cancellationToken);
    }
}