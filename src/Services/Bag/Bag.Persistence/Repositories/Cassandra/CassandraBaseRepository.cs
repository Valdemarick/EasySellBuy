using Bag.Application.Cassandra.Interfaces;
using Cassandra;
using Cassandra.Mapping;

namespace Bag.Persistence.Repositories.Cassandra;
public abstract class CassandraBaseRepository<TEntity> : ICassandraBaseRepository<TEntity>
{
    private readonly ICluster _cassandraCluster;

    private ISession _session;
    private IMapper _mapper;

    protected ISession Session => _session ??= _cassandraCluster.Connect(Keyspace);
    protected IMapper Mapper => _mapper ??= new Mapper(Session);

    public CassandraBaseRepository(ICluster cassandraCluster)
    {
        _cassandraCluster = cassandraCluster ?? throw new ArgumentNullException(nameof(cassandraCluster));
    }

    protected abstract string Keyspace { get; }

    public async Task<IEnumerable<TEntity>> GetAsync() => await Mapper.FetchAsync<TEntity>();

    public async Task InsertAsync(TEntity model) => await Mapper.InsertAsync(model);

    public void Dispose() => _session?.Dispose();
}