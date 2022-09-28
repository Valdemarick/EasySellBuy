using Bag.Application.Common.Interfaces.Services;
using Bag.Application.Dtos.Sellers;
using Microsoft.AspNetCore.Mvc;

namespace Bag.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SellersController : ControllerBase
{
    private readonly ISellerService _service;

    public SellersController(ISellerService service)
    {
        _service = service ?? throw new ArgumentNullException(nameof(service));
    }

    /// <summary>
    /// Returns a list of sellers
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<SellerReadModel>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<SellerReadModel>>> GetAsync(CancellationToken cancellationToken)
    {
        var orders = await _service.GetAsync(cancellationToken);

        return Ok(orders);
    }

    /// <summary>
    /// Returns a seller by id
    /// </summary>
    /// <param name="id">seller id</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(SellerReadModel), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<SellerReadModel>> GetAsync([FromRoute] int id, CancellationToken cancellationToken)
    {
        var order = await _service.GetAsync(id, cancellationToken);
        if (order is null)
        {
            return NotFound();
        }

        return Ok(order);
    }

    /// <summary>
    /// Created a new seller
    /// </summary>
    /// <param name="model">payload</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> CreateAsync([FromBody] SellerCreateModel model, CancellationToken cancellationToken)
    {
        var created = await _service.CreateAsync(model, cancellationToken);

        return Created(nameof(GetAsync), created);
    }

    /// <summary>
    /// Updates a seller by id and payload
    /// </summary>
    /// <param name="id">seller id</param>
    /// <param name="model">payload</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> UpdateAsync([FromRoute] int id, [FromBody] SellerUpdateModel model, CancellationToken cancellationToken)
    {
        await _service.UpdateAsync(id, model, cancellationToken);

        return NoContent();
    }

    /// <summary>
    /// Deletes a seller by id
    /// </summary>
    /// <param name="id">seller id</param>
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