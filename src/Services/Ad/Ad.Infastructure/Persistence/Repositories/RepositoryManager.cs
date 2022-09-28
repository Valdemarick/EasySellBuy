using Ad.Application.Interfaces.Repositories;
using Ad.Infrastructure.Persistence.Contexts;

namespace Ad.Infrastructure.Persistence.Repositories;

public class RepositoryManager : IRepositoryManager
{
    private readonly ApplicationDbContext _context;

    private IAdRepository _adRepository;

    public RepositoryManager(ApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public IAdRepository AdRepository => 
        _adRepository ??= new AdRepository(_context);

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }
}