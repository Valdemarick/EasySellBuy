using Bag.Application.Cassandra.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Shared.Models;

namespace Bag.WebApi.Controllers;

[Route("api/[controller]s")]
[ApiController]
public class CassandraController : ControllerBase
{
    private readonly ICassandraOrderRepository _cassandraRepo;

    public CassandraController(ICassandraOrderRepository cassandraRepository)
    {
        _cassandraRepo = cassandraRepository ?? throw new ArgumentNullException(nameof(cassandraRepository));
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<OrderCassandra>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<OrderCassandra>>> GetAsync()
    {
        var orders = await _cassandraRepo.GetAsync();

        return Ok(orders);
    }
}