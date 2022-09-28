using Confluent.Kafka;
using Microsoft.Extensions.Options;

namespace EventBus.Kafka.Producer;

public class KafkaProducer<TKey, TValue> : IKafkaProducer<TKey, TValue>
{
    private readonly IProducer<TKey, TValue> _producer;
    private readonly string _topic;

    public KafkaProducer(IProducer<TKey, TValue> producer, IOptions<KafkaProducerConfig<TKey, TValue>> topicOptions)
    {
        _producer = producer;
        _topic = topicOptions.Value.Topic;
    }

    public async Task ProduceAsync(TKey key, TValue value)
    {
        await _producer.ProduceAsync(_topic, new Message<TKey, TValue> { Key = key, Value = value });
    }

    public void Dispose() => _producer.Dispose();
}