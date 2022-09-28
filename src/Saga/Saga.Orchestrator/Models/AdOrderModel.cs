using Ad.Application.Ads.Commands.Create;
using Bag.Application.Dtos.Orders;

namespace Saga.Orchestrator.Models;

public class AdOrderModel
{
    public CreateAdCommand Ad { get; set; } = null!;
    public OrderCreateModel Order { get; set; } = null!;
}
