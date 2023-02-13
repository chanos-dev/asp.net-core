using Microsoft.Extensions.Caching.Memory;
using RedisTutorial.Model;
using StackExchange.Redis;
using System.Reflection;

namespace RedisTutorial.Services;

public class RedisService
{
    private readonly IConnectionMultiplexer _connectionMultiplexer;
    private readonly IDatabase _database;

    public RedisService(IConnectionMultiplexer connectionMultiplexer)
    {
        _connectionMultiplexer = connectionMultiplexer;
        _database = _connectionMultiplexer.GetDatabase();
    }

    public async Task<string?> GetCacheValueAsync(string key)
    { 
        return await _database.StringGetAsync(key);
    }

    public async Task SetCacheValueAsync(string key, string value)
    { 
        await _database.StringSetAsync(key, value);
    }

    public async Task SetCacheValueWtihTimeoutAsync(string key, string value, int timeout)
    { 
        await _database.StringSetAsync(key, value, TimeSpan.FromSeconds(timeout));
    }

    public async Task<MapData> GetCacheAllMapAsync(string key)
    {        
        object ConvertValue(PropertyInfo prop, string data)
        {
            if (prop.PropertyType == typeof(int))
                return int.Parse(data);

            return data;
        }

        // O(N)
        HashEntry[] entries = await _database.HashGetAllAsync(key);
        MapData data = new MapData();

        var props = data.GetType().GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public);

        foreach (var entry in entries)
        {
            var prop = props.SingleOrDefault(p => p.Name == entry.Name);
            if (prop is null)
                continue;

            prop.SetValue(data, ConvertValue(prop, entry.Value!));
        }

        return data;
    }

    public async Task<string?> GetCacheMapFieldAsync(string key, string field)
    {
        // O(1)
        return await _database.HashGetAsync(key, field);
    }

    public async Task<bool> ExistKeyAsync(string key)
    { 
        return await _database.KeyExistsAsync(key);
    }

    internal async Task SetCacheMapAsync(string key, MapData value)
    { 
        foreach(var prop in value.GetType().GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public))
        {
            var propValue = prop.GetValue(value);
            if (propValue is null) 
                continue;

            await _database.HashSetAsync(key, prop.Name, propValue.ToString());
        }
    }
}