using Bag.Application.Cassandra.Interfaces;
using EventBus.Kafka.Consumer;
using Shared.Constants;
using Shared.Models;

namespace Bag.Application.Handlers;

public class OrderCreatedHandler : IKafkaConsumerHandler<string, OrderCassandra>
{
    private readonly ICassandraOrderRepository _cassandraOrderRepo;

    public OrderCreatedHandler(ICassandraOrderRepository cassandraOrderRepository)
    {
        _cassandraOrderRepo = cassandraOrderRepository;
    }

    public async Task HandleAsync(string key, OrderCassandra value)
    {
        if (key == KafkaKeys.CassandraKey)
        {
            await _cassandraOrderRepo.InsertAsync(value);
        }
    }
}