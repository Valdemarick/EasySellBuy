using Bag.Application.Cassandra.Interfaces;
using Cassandra;
using Shared.Models;

namespace Bag.Persistence.Repositories.Cassandra;

public class CassandraOrderRepository : CassandraBaseRepository<OrderCassandra>, ICassandraOrderRepository
{
    protected override string Keyspace => "easysellbuy";

    public CassandraOrderRepository(ICluster cassandraCluster) : base(cassandraCluster) { }
}