using Ad.Application.Ads.Commands.Create;
using Ad.Application.Ads.Commands.Delete;
using Ad.Application.Ads.Commands.Update;
using Ad.Application.Ads.Queries.GetAd;
using Ad.Application.Ads.Queries.GetAds;
using Ad.Application.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ad.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AdsController : ControllerBase
{
    private readonly IMediator _mediator;

    public AdsController(IMediator mediator) => _mediator = mediator;

    /// <summary>
    /// Returns a list of ads
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<AdReadModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<AdReadModel>>> GetAsync(CancellationToken cancellationToken)
    {
        var ads = await _mediator.Send(new GetAdsQuery(), cancellationToken);

        return Ok(ads);
    }

    /// <summary>
    /// Returns an ad by id
    /// </summary>
    /// <param name="request">ad id</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(AdReadModel), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<AdReadModel>> GetAsync([FromRoute] GetAdQuery request, CancellationToken cancellationToken)
    {
        var ad = await _mediator.Send(request, cancellationToken);
        if (ad is null)
        {
            return NotFound();
        }

        return Ok(ad);
    }

    /// <summary>
    /// Creates a new ad
    /// </summary>
    /// <param name="request">payload</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(typeof(AdReadModel), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<int>> CreateAsync([FromBody] CreateAdCommand request, CancellationToken cancellationToken)
    {
        var createdAd = await _mediator.Send(request, cancellationToken);

        return Created(nameof(GetAsync), createdAd);
    }

    /// <summary>
    /// Updates an ad by id and payload
    /// </summary>
    /// <param name="id">ad id</param>
    /// <param name="request">payload</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> UpdateAsync([FromRoute] int id, [FromBody] UpdateAdCommand request, CancellationToken cancellationToken)
    {
        request.Id = id;
        await _mediator.Send(request, cancellationToken);

        return NoContent();
    }

    /// <summary>
    /// Deletes an ad dby id
    /// </summary>
    /// <param name="request">ad id</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> DeleteAsync([FromRoute] DeleteAdCommand request, CancellationToken cancellationToken)
    {
        await _mediator.Send(request, cancellationToken);

        return NoContent();
    }
}