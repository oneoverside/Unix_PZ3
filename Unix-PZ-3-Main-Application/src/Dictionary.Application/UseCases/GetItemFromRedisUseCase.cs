using Dictionary.Application.Interfaces.Stores;
using Dictionary.Application.Interfaces.UseCases;
using Dictionary.Application.Models;
using Microsoft.Extensions.Logging;

namespace Dictionary.Application.UseCases;

class GetItemFromRedisUseCase : IGetItemFromRedisUseCase
{
    private readonly IRedisManager _redisManager;
    private readonly FileSystemLogger _logger;

    public GetItemFromRedisUseCase(IRedisManager redisManager, FileSystemLogger logger)
    {
        _redisManager = redisManager;
        _logger = logger;
    }

    public async Task<InvenItem?> Execute(Guid itemId)
    {
        try
        {
            _logger.LogInfo("Try get inven item from redis");
            var result =  await _redisManager.GetInvenItem(itemId);
            if (result is null)
            {
                _logger.LogWarning("Can't get correct info for incoming data");
            }
            else
            {
                _logger.LogSuccess("Successfully get");
            }
            
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError("Something go wrong", ex);
            return null;
        }
    }
}