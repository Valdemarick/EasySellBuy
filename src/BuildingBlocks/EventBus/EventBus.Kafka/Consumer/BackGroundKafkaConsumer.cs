using Confluent.Kafka;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace EventBus.Kafka.Consumer;

public class BackGroundKafkaConsumer<TKey, TValue> : BackgroundService
{
    private readonly KafkaConsumerConfig<TKey, TValue> _config;
    private IKafkaConsumerHandler<TKey, TValue> _consumerHandler;
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public BackGroundKafkaConsumer(IOptions<KafkaConsumerConfig<TKey, TValue>> config, IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
        _config = config.Value;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var scope = _serviceScopeFactory.CreateScope();
        _consumerHandler = scope.ServiceProvider.GetRequiredService<IKafkaConsumerHandler<TKey, TValue>>();

        var builder = new ConsumerBuilder<TKey, TValue>(_config).SetValueDeserializer(new KafkaDeserializer<TValue>());

        using var consumer = builder.Build();
        consumer.Subscribe(_config.Topic);

        while (!stoppingToken.IsCancellationRequested)
        {
            await Task.Delay(5);
            var result = consumer.Consume(TimeSpan.FromMilliseconds(10));

            if (result != null)
            {
                await _consumerHandler.HandleAsync(result.Message.Key, result.Message.Value);

                consumer.Commit(result);
                consumer.StoreOffset(result);
            }
        }
    }
}