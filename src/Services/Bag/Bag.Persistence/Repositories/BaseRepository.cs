using Bag.Application.Common.Interfaces.Repositories;
using Bag.Domain.Entities;
using Bag.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Bag.Persistence.Repositories;

public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity>
    where TEntity : BaseEntity
{
    protected readonly ApplicationDbContext сontext;
    protected readonly DbSet<TEntity> dbSet;

    public BaseRepository(ApplicationDbContext context)
    {
        сontext = context ?? throw new ArgumentNullException(nameof(context));
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

    public virtual async Task<TEntity> CreateAsync(TEntity model, CancellationToken cancellationToken = default)
    {
        var created = await dbSet.AddAsync(model, cancellationToken);

        return created.Entity;
    }

    public virtual void Update(TEntity model)
    {
        dbSet.Update(model);
    }

    public virtual void Delete(TEntity entity)
    {
        dbSet.Remove(entity);
    }

    public virtual async Task<TEntity?> FindAsync(
        Expression<Func<TEntity, bool>> predicate, 
        CancellationToken cancellationToken = default)
    {
        return await dbSet.Where(predicate).FirstOrDefaultAsync(cancellationToken);
    }
}