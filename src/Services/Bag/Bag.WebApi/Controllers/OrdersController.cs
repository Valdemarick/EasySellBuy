using Bag.Application.Common.Interfaces.Services;
using Bag.Application.Dtos.Orders;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;

namespace Bag.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrdersController : ControllerBase
{
    private readonly IOrderService _service;

    public OrdersController(IOrderService service)
    {
        _service = service ?? throw new ArgumentNullException(nameof(service));
    }

    /// <summary>
    /// Returns a list of orders
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet]
    [EnableQuery]
    [ProducesResponseType(typeof(IEnumerable<OrderReadModel>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<OrderReadModel>>> GetAsync(CancellationToken cancellationToken)
    {
        var orders = await _service.GetAsync(cancellationToken);

        return Ok(orders);
    }

    /// <summary>
    /// Returns three random orders and caches them in Redis
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("getrandomly")]
    public async Task<ActionResult<IEnumerable<OrderReadModel>>> GetRandomlyAsync(CancellationToken cancellationToken) 
    {
        var key = nameof(OrdersController);

        var randomOrders = await _service.GetRandomlyAsync(key, cancellationToken);

        return Ok(randomOrders);
    }

    /// <summary>
    /// Distributed lock 
    /// </summary>
    /// <returns></returns>
    [HttpGet("lock")]
    public async Task<ActionResult> GetAsync()
    {
        await _service.GetLock();

        return Ok();
    }

    /// <summary>
    /// Returns an order by id
    /// </summary>
    /// <param name="id">order id</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(OrderReadModel), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<OrderReadModel>> GetAsync([FromRoute] int id, CancellationToken cancellationToken)
    {
        var order = await _service.GetAsync(id, cancellationToken);
        if (order is null)
        {
            return NotFound();
        }

        return Ok(order);
    }

    /// <summary>
    /// Creates a new order
    /// </summary>
    /// <param name="model">payload</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<int>> CreateAsync([FromBody] OrderCreateModel model, CancellationToken cancellationToken)
    {
        var created = await _service.CreateAsync(model, cancellationToken);

        return Created(nameof(GetAsync), created);
    }

    /// <summary>
    /// Updates an order by id and payload
    /// </summary>
    /// <param name="id">order id</param>
    /// <param name="model">payload</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> UpdateAsync([FromRoute] int id, [FromBody] OrderUpdateModel model, CancellationToken cancellationToken)
    {
        await _service.UpdateAsync(id, model, cancellationToken);

        return NoContent();
    }

    /// <summary>
    /// Deletes an order by id
    /// </summary>
    /// <param name="id">order id</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> DeleteAsync([FromRoute] int id, CancellationToken cancellationToken)
    {
        await _service.DeleteAsync(id, cancellationToken);

        return NoContent();
    }
}