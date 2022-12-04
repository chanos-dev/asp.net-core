using Microsoft.Extensions.Caching.Memory;
using StackExchange.Redis;

namespace RedisTutorial.Services;

public class RedisService
{
    private readonly IConnectionMultiplexer _connectionMultiplexer;

    public RedisService(IConnectionMultiplexer connectionMultiplexer)
    {
        this._connectionMultiplexer = connectionMultiplexer;
    }

    public async Task<string?> GetCacheValueAsync(string key)
    {
        var db = this._connectionMultiplexer.GetDatabase();
        return await db.StringGetAsync(key);
    }

    public async Task SetCacheValueAsync(string key, string value)
    {
        var db = this._connectionMultiplexer.GetDatabase();
        await db.StringSetAsync(key, value);
    }
}