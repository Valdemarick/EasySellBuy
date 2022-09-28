using Microsoft.AspNetCore.Mvc;
using Saga.Orchestrator.Models;
using Saga.Orchestrator.Services;

namespace Saga.Orchestrator.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SagaController : ControllerBase
{
    private readonly IOrchestrator _orchestrator;

    public SagaController(IOrchestrator orchestrator)
    {
        _orchestrator = orchestrator ?? throw new ArgumentNullException(nameof(orchestrator));
    }

    [HttpPost]
    public async Task<ActionResult> CreateOrderAsync(
        [FromBody] AdOrderModel model,
        CancellationToken cancellationToken)
    {
        var areCreated = await _orchestrator.TryCreateAdAndOrderAsync(model.Ad, model.Order, cancellationToken);
        if (!areCreated)
        {
            return BadRequest();
        }

        return StatusCode(201);
    }
}