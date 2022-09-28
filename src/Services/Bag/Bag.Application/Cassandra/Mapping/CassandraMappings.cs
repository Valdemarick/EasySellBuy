using Cassandra.Mapping;
using Shared.Models;

namespace Bag.Application.Cassandra.Mapping;

public class CassandraMappings : Mappings
{
    public CassandraMappings()
    {
        For<OrderCassandra>().TableName("orders").PartitionKey(m => m.Id)
            .Column(m => m.Id)
            .Column(m => m.Name);
    }
}