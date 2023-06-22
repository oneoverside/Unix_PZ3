using System.Globalization;
using Dictionary.Application.Interfaces.Stores;
using Dictionary.Application.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace Dictionary.Infrastructure.Stores;

internal class RedisManager : IRedisManager
{
    private readonly IDatabase _database;
    private readonly FileSystemLogger _logger;

    public RedisManager(IDatabase database, FileSystemLogger logger)
    {
        _database = database;
        _logger = logger;
    }

    public async Task<InvenItem?> GetInvenItem(Guid itemId, CancellationToken cancellationToken = default)
    {
        var result = await _database.StringGetAsync(itemId.ToString());
        return JsonConvert.DeserializeObject<InvenItem>(result!);
    }

    public async Task<DateTime> GetLastSyncDate()
    {
        try
        {
            var serializedDate = (await _database.StringGetAsync("LAST_SYNC_DATE")).ToString();
            _logger.LogSuccess($"Successfully get value from redis: {serializedDate}");
            return DateTime.Parse(serializedDate).ToUniversalTime();
        }
        catch
        {
            _logger.LogWarning($"Something go wrong so min date will be return: {DateTime.MinValue.ToUniversalTime()}");
            return DateTime.MinValue.ToUniversalTime();
        }
    }
    
    public async Task Set(Dictionary<string, object> dictionaryForUpdateCache)
    {
        foreach (var pair in dictionaryForUpdateCache)
        {
            await Set(pair.Key, JsonConvert.SerializeObject(pair.Value));
        }
        _logger.LogInfo("Successfully add all pairs to Redis");
        await _database.StringSetAsync("LAST_SYNC_DATE", DateTime.Now.ToString(CultureInfo.InvariantCulture));
        _logger.LogInfo($"Successfully set new last sync date: {DateTime.Now.ToString(CultureInfo.InvariantCulture)}");
    }

    public async Task Set(string key, string value)
    {
        await _database.StringSetAsync(key, value);
        _logger.LogInfo("Successfully add pairs to Redis");
    }
    
    public async Task<string> Get(string key)
    {
        var result = (await _database.StringGetAsync(key)).ToString();
        _logger.LogInfo("Successfully get value from Redis");
        return result;
    }
}