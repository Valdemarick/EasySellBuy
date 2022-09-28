namespace Bag.Application.Cassandra.Interfaces;

public interface ICassandraBaseRepository<TEntity> : IDisposable
{
    Task<IEnumerable<TEntity>> GetAsync();
    Task InsertAsync(TEntity model);
}