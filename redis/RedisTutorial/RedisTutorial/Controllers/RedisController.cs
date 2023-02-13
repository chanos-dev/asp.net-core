using Microsoft.AspNetCore.Mvc;
using RedisTutorial.Model;
using RedisTutorial.Services;

namespace RedisTutorial.Controllers;

[ApiController]
[Route("[controller]")]
public class RedisController : ControllerBase
{
    private readonly RedisService _redisService;
    
    public RedisController(RedisService redisService)
    {
        _redisService = redisService;
    }

    [HttpGet("{key}")]
    public async Task<IActionResult> Get([FromRoute] string key)
    {
        var value = await _redisService.GetCacheValueAsync(key);
        return string.IsNullOrEmpty(value) ? NotFound() : Ok(value);
    }

    [HttpGet]
    [Route("allmap/{key}")]
    public async Task<IActionResult> GetAllMap([FromRoute] string key)
    {
        MapData data = await _redisService.GetCacheAllMapAsync(key);
        return data is not null ? Ok(data) : NotFound();
    }

    [HttpGet]
    [Route("map/{key}/{field}")]
    public async Task<IActionResult> GetMap([FromRoute] string key, [FromRoute] string field)
    {
        string? data = await _redisService.GetCacheMapFieldAsync(key, field);
        return data is not null ? Ok(data) : NotFound();
    }

    [HttpGet]
    [Route("exist/{key}")]
    public async Task<IActionResult> Exist([FromRoute] string key)
    {
        return await _redisService.ExistKeyAsync(key) ? Ok() : NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CacheRequest request)
    {
        await _redisService.SetCacheValueAsync(request.Key, request.Value);
        return CreatedAtAction("Get", new { key = request.Key }, request);
    }

    [HttpPost]
    [Route("withTimeout")]
    public async Task<IActionResult> PostWithTimeout([FromBody] CacheRequestWithTimeout request)
    {
        await _redisService.SetCacheValueWtihTimeoutAsync(request.Key, request.Value, request.Timeout);
        return CreatedAtAction("Get", new { key = request.Key }, request);
    }

    [HttpPost]
    [Route("map")]
    public async Task<IActionResult> PostMap([FromBody] CacheRequestMap request)
    {
        await _redisService.SetCacheMapAsync(request.Key, request.Value);
        return CreatedAtAction("Get", new { key = request.Key }, request);
    }
}