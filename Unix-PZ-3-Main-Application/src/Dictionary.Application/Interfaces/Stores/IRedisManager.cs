using Dictionary.Application.Models;

namespace Dictionary.Application.Interfaces.Stores;

public interface IRedisManager
{
    public Task<InvenItem?> GetInvenItem(Guid itemId, CancellationToken cancellationToken = default);
    Task<DateTime> GetLastSyncDate();
    Task Set(Dictionary<string, object> dictionaryForUpdateCache);

    Task Set(string key, string value);
    Task<string> Get(string key);
}