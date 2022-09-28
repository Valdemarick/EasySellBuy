using Ad.Application.Ads.Commands.Create;
using Bag.Application.Dtos.Orders;

namespace Saga.Orchestrator.Services;

public interface IOrchestrator
{
    Task<bool> TryCreateAdAndOrderAsync(CreateAdCommand command, OrderCreateModel order, CancellationToken cancellationToken = default);
}