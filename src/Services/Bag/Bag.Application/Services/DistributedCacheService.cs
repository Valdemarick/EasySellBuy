using Bag.Application.Common.Interfaces.Services;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Bag.Application.Services;

public class DistributedCacheService : IDistributedCacheService
{
    private readonly IDistributedCache _distributedCache;

    public DistributedCacheService(IDistributedCache distributedCache)
    {
        _distributedCache = distributedCache ?? throw new ArgumentNullException(nameof(distributedCache));
    }

    public async Task<T?> GetAsync<T>(string key, CancellationToken cancellationToken = default)
    {
        var bytes = await _distributedCache.GetAsync(key, cancellationToken);
        if (bytes is null)
        {
            return default(T);
        }

        return await JsonSerializer.DeserializeAsync<T>(new MemoryStream(bytes));
    }

    public async Task SetAsync<T>(string key, T value, DistributedCacheEntryOptions options, CancellationToken cancellationToken = default)
    {
        var bytes = JsonSerializer.SerializeToUtf8Bytes(value);

        await _distributedCache.SetAsync(key, bytes, options, cancellationToken);
    }
}