using Confluent.Kafka;

namespace EventBus.Kafka.Producer;

public class KafkaProducerConfig<TKey, TValue> : ProducerConfig
{
    public string Topic { get; set; } = null!;
}