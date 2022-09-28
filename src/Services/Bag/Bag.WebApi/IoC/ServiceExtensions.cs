using Bag.Application.Handlers;
using Confluent.Kafka;
using EventBus.Kafka.IoC;
using Shared.Models;

namespace Bag.WebApi.IoC;

public static class ServiceExtensions
{
    public static IServiceCollection AddKafka(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddKafkaConsumer<string, OrderCassandra, OrderCreatedHandler>(broker =>
        {
            broker.Topic = configuration["Kafka:Topic"];
            broker.GroupId = configuration["Kafka:GroupId"];
            broker.BootstrapServers = configuration["Kafka:BootstrapServers"];
            broker.AutoOffsetReset = AutoOffsetReset.Latest;
        });

        return services;
    }
}