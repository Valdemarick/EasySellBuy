using System.Linq.Expressions;

namespace Ad.Application.Interfaces.Repositories;

public interface IBaseRepository<TEntity>
{
    Task<IEnumerable<TEntity>> GetAsync(CancellationToken cancellationToken = default);
    Task<TEntity?> GetAsync(int id, CancellationToken cancellationToken = default);
    Task<TEntity> CreateAsync(TEntity entity, CancellationToken cancellationToken = default);
    void Update(TEntity entity);
    void Delete(TEntity entity);
    Task<TEntity?> FindAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);
}