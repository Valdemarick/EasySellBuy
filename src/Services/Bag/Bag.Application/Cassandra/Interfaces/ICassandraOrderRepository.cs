using Shared.Models;

namespace Bag.Application.Cassandra.Interfaces;

public interface ICassandraOrderRepository : ICassandraBaseRepository<OrderCassandra> { }