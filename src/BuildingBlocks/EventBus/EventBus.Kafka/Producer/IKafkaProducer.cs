namespace EventBus.Kafka.Producer;

public interface IKafkaProducer<TKey, TValue> : IDisposable
{
    Task ProduceAsync(TKey key, TValue value);
}