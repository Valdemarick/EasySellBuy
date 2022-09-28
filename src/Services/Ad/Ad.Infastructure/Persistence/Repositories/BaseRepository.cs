using Ad.Application.Interfaces.Repositories;
using Ad.Domain.Models;
using Ad.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Ad.Infrastructure.Persistence.Repositories;

public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseModel
{
    protected readonly ApplicationDbContext context;
    protected readonly DbSet<TEntity> dbSet;

    public BaseRepository(ApplicationDbContext context)
    {
        this.context = context ?? throw new ArgumentNullException(nameof(context));
        dbSet = context.Set<TEntity>();
    }

    public virtual async Task<IEnumerable<TEntity>> GetAsync(CancellationToken cancellationToken = default)
    {
        return await dbSet.AsNoTracking().ToListAsync(cancellationToken);
    }

    public virtual async Task<TEntity?> GetAsync(int id, CancellationToken cancellationToken = default)
    {
        return await dbSet.AsNoTracking().SingleOrDefaultAsync(m => m.Id == id, cancellationToken);
    }

    public virtual async Task<TEntity> CreateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        var created =  await dbSet.AddAsync(entity, cancellationToken);

        return created.Entity;
    }

    public virtual void Update(TEntity entity)
    {
        dbSet.Update(entity);
    }

    public virtual void Delete(TEntity entity)
    {
        dbSet.Remove(entity);
    }

    public async Task<TEntity?> FindAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return await dbSet.Where(predicate).FirstOrDefaultAsync(cancellationToken);
    }
}