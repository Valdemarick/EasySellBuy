using EventBus.Kafka.IoC;
using Shared.Models;

namespace Ad.WebApi.IoC;

public static class ServiceExtensions
{
    public static IServiceCollection AddKafka(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddKafkaProducer<string, OrderCassandra>(producer =>
        {
            producer.Topic = configuration["Kafka:Topic"];
            producer.BootstrapServers = configuration["Kafka:BootstrapServers"];
        });

        return services;
    }
}
