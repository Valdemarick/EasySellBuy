using Ad.Application.Ads.Commands.Create;
using Ad.Application.Dtos;
using Bag.Application.Dtos.Orders;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Saga.Orchestrator.Models;
using Saga.Orchestrator.Models.Clients;
using System.Net;
using System.Text;

namespace Saga.Orchestrator.Services;

public class CustomOrchestrator : IOrchestrator
{
    private readonly ILogger<CustomOrchestrator> _logger;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly AdClientOptions _adClientOptions;
    private readonly BagClientOptions _bagClientOptions;
    private readonly HttpClient _adClient;
    private readonly HttpClient _bagClient;

    public CustomOrchestrator(
        ILogger<CustomOrchestrator> logger,
        IHttpClientFactory httpClientFactory,
        IOptions<AdClientOptions> adClientOptions,
        IOptions<BagClientOptions> bagClientOptions)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
        _adClientOptions = adClientOptions.Value ?? throw new ArgumentException(nameof(adClientOptions));
        _bagClientOptions = bagClientOptions.Value ?? throw new ArgumentException(nameof(bagClientOptions));

        _adClient = _httpClientFactory.CreateClient(_adClientOptions.Name);
        _bagClient = _httpClientFactory.CreateClient(_bagClientOptions.Name);
    }

    public async Task<bool> TryCreateAdAndOrderAsync(CreateAdCommand command, OrderCreateModel order, CancellationToken cancellationToken = default)
    {
        var creatingAdResult = await PostAdAsync(command, cancellationToken);
        if (!creatingAdResult!.Success)
        {
            return false;
        }

        var creatingOrderResult = await PostOrderAsync(order, cancellationToken);
        if (!creatingOrderResult!.Success)
        {
            await DeleteAdAsync(creatingAdResult.Value, cancellationToken);

            return false;
        }

        return true;
    }

    private async Task<Result<int>> PostAdAsync(CreateAdCommand command, CancellationToken cancellationToken = default)
    {
        try
        {
            var response = await _adClient.PostAsync(
                $"/api/ads",
                new StringContent(JsonConvert.SerializeObject(command), Encoding.UTF8, "application/JSON"),
                cancellationToken);
            if (response.StatusCode != HttpStatusCode.Created)
            {
                return new Result<int>(response.ReasonPhrase);
            }

            var createdAd = await response.Content.ReadFromJsonAsync<AdReadModel>();

            return new Result<int>(createdAd!.Id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);

            return new Result<int>(ex.Message);
        }
    }

    private async Task<Result<int>?> PostOrderAsync(OrderCreateModel order, CancellationToken cancellationToken = default)
    {
        try
        {
            var response = await _bagClient.PostAsync(
                $"/api/orders",
                new StringContent(JsonConvert.SerializeObject(order), Encoding.UTF8, "application/JSON"),
                cancellationToken);
            if (response.StatusCode != HttpStatusCode.Created)
            {
                return new Result<int>(response.ReasonPhrase);
            }

            var createdOrder = await response.Content.ReadFromJsonAsync<OrderReadModel>();

            return new Result<int>(createdOrder!.Id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);

            return new Result<int>(ex.Message);
        }
    }

    private async Task DeleteAdAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            var response = await _adClient.DeleteAsync($"/api/ads/{id}", cancellationToken);
            if (response.StatusCode != HttpStatusCode.NoContent)
            {
                _logger.LogWarning($"The ad (id:{id}) hasn't been deleted");
            }
        }
        catch(Exception ex)
        {
            _logger.LogError(ex.Message);
        }
    }
}