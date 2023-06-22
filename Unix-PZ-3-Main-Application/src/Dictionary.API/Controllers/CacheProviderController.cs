using Dictionary.Application.Interfaces.Stores;
using Dictionary.Application.Interfaces.UseCases;
using Dictionary.Application.Models;
using Microsoft.AspNetCore.Mvc;

namespace Dictionary.API.Controllers;

[ApiController]
public class CacheProviderController : ControllerBase
{
    [HttpGet("UpdateCache")]
    public async Task<OkResult> UpdateCache([FromServices] IUpdateCacheUseCase useCase, FileSystemLogger logger, ILogger<CacheProviderController> logger2, ILogger<CacheProviderController> cs)
    {
        try
        {
            logger2.LogError($"Logger path: {logger._path}");
            logger.ActionName = "UpdateCache";
            logger.LogInfo("Start cache updating");
            await useCase.Execute();
        }
        catch(Exception ex)
        {
            logger2.LogError("Something go wrong", ex);
        }
        return Ok();
    }

    [HttpGet("Get/InvenItem/{id:guid}")]
    public async Task<OkObjectResult> GetItem([FromRoute]Guid id, [FromServices] IGetItemFromRedisUseCase useCase, [FromServices] FileSystemLogger logger)
    {
        logger.ActionName = $"GetInvenItem_{id}";
        logger.LogInfo("Start getting");
        var result = await useCase.Execute(id);
        
        return Ok(result);
    }
    
    [HttpPost("Update/AddInvenItem")]
    public async Task<OkResult> SetItem([FromBody]InvenItem item, [FromServices] IUpdateInvenItemUseCase useCase, [FromServices] FileSystemLogger logger)
    {
        logger.ActionName = "AddInvenItem";
        logger.LogInfo("Start cache updating");
        await useCase.Execute(item);
        return Ok();
    }
    
    [HttpPost("GetConnectionString")]
    public OkObjectResult SetItem12([FromServices] IConfiguration config)
    {
        var s = config.GetValue<string>("ConnectionStrings:PostgresConnection");
        return Ok(s);
    }
    
    [HttpPost("GetRedisConnectionString")]
    public OkObjectResult SetItem23([FromServices] IConfiguration config)
    {
        var s = config.GetValue<string>("ConnectionStrings:RedisConnection");
        return Ok(s);
    }
    
    [HttpPost("GetLoggerString")]
    public OkObjectResult SetItem24([FromServices] IConfiguration config)
    {
        var s = config.GetValue<string>("Logging:LogFolderPath");
        return Ok(s);
    }
    
    [HttpPost("SetRedis")]
    public async Task<OkResult> SetItem1([FromBody] Pair pair, [FromServices] IRedisManager redis)
    {
        await redis.Set(pair.Key, pair.Value);
        return Ok();
    }
    
    [HttpPost("GetRedis")]
    public async Task<OkObjectResult> SetItem2([FromBody] string key, [FromServices] IRedisManager redis)
    {
        return Ok(await redis.Get(key));
    }
    
    public class Pair
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }
}