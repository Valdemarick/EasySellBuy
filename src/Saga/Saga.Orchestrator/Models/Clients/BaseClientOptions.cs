namespace Saga.Orchestrator.Models.Clients;

public abstract class BaseClientOptions
{
    public string Name { get; init; } = null!;
    public string Url { get; init; } = null!;
}