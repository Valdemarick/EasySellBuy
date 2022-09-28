using System.Linq.Expressions;

namespace Bag.Application.Common.Interfaces.Repositories;

public interface IBaseRepository<TEntity>
{
    Task<IEnumerable<TEntity>> GetAsync(CancellationToken cancellationToken = default);
    Task<TEntity?> GetAsync(int id, CancellationToken cancellationToken = default);
    Task<TEntity?> FindAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);
    Task<TEntity> CreateAsync(TEntity model, CancellationToken cancellationToken = default);
    void Update(TEntity model);
    void Delete(TEntity entity);
}