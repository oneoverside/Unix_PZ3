using Dictionary.Application.Models;

namespace Dictionary.Application.Interfaces.Stores;

public interface IPostgresReadonlyStore
{
    Task<ContentForUpdate> GetContentForUpdate(DateTime lastSyncDate);

    public Task AddInvenItem(InvenItem item);

    public Task<InvenItem?> GetInvenItem(Guid itemId);
    
    public class ContentForUpdate
    {
        public List<InvenItem> Items { get; set; }
        
        public Dictionary<string, object> GetAsCacheDictionary()
        {
            var result = new Dictionary<string, object>();
            foreach (var item in Items)
            {
                result[item.InvenItemId.ToString()] = item;
            }
            return result;
        }
    }
}