using Dictionary.Application.Interfaces.Stores;
using Dictionary.Application.Interfaces.UseCases;
using Dictionary.Application.Models;
using Microsoft.Extensions.Logging;

namespace Dictionary.Application.UseCases;

class UpdateCacheUseCase : IUpdateCacheUseCase
{
    private readonly IPostgresReadonlyStore _postgresStore;
    private readonly IRedisManager _redis;
    private readonly FileSystemLogger _logger;
    
    public UpdateCacheUseCase(IPostgresReadonlyStore postgresStore, IRedisManager redis, FileSystemLogger logger)
    {
        _postgresStore = postgresStore;
        _redis = redis;
        _logger = logger;
    }

    public async Task Execute()
    {
        try
        {
            _logger.LogInfo("Try get last sync date");
            var lastSyncDate = await _redis.GetLastSyncDate();
            _logger.LogSuccess($"Get last sync date: {lastSyncDate}");
            
            _logger.LogInfo("Try get required values from DB");
            var contentForUpdate = await _postgresStore.GetContentForUpdate(lastSyncDate);
            _logger.LogSuccess("Successfully get info from DB");
            
            var dictionaryForUpdateCache = contentForUpdate.GetAsCacheDictionary();
            
            _logger.LogInfo("Try add new values into redis");
            await _redis.Set(dictionaryForUpdateCache);
            _logger.LogSuccess("Successfully add");

        }
        catch (Exception ex)
        {
            _logger.LogError("Something go wrong", ex);
        }
    }
}