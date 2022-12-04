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

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CacheRequest request)
    {
        await _redisService.SetCacheValueAsync(request.Key, request.Value);
        return CreatedAtAction("Get", new { key = request.Key }, request);
    }
}