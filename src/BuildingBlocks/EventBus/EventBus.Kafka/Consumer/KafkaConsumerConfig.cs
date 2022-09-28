using Confluent.Kafka;

namespace EventBus.Kafka.Consumer;

public class KafkaConsumerConfig<TKey, TValue> : ConsumerConfig
{
    public string Topic { get; set; } = null!;

    public KafkaConsumerConfig()
    {
        AutoOffsetReset = Confluent.Kafka.AutoOffsetReset.Earliest;
        EnableAutoOffsetStore = false;
    }
}