using EventBus.Kafka.Producer;
using Microsoft.AspNetCore.Mvc;
using Shared.Constants;
using Shared.Models;

namespace Ad.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class KafkaController
{
    private readonly IKafkaProducer<string, OrderCassandra> _producer;

    public KafkaController(IKafkaProducer<string, OrderCassandra> producer)
    {
        _producer = producer;
    }

    [HttpGet]
    public async Task ProduceAsync()
    {
        for (int i = 0; i < 100; i++)
        {
            await _producer.ProduceAsync(KafkaKeys.CassandraKey, new OrderCassandra() { Id = i, Name = "smth" });
        }
    }
}