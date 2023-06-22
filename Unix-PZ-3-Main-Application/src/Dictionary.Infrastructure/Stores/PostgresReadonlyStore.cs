using Dictionary.Application.Interfaces.Stores;
using Dictionary.Application.Models;
using Dictionary.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Dictionary.Infrastructure.Stores;

class PostgresReadonlyStore : IPostgresReadonlyStore
{
    private readonly PostgresDbContext _dbContext;
    private readonly FileSystemLogger _logger;
    
    public PostgresReadonlyStore(PostgresDbContext dbContext, FileSystemLogger logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<IPostgresReadonlyStore.ContentForUpdate> GetContentForUpdate(DateTime lastSyncDate)
    {
        _logger.LogInfo($"Try to get all items with data >= {lastSyncDate}");
        var x1 = await _dbContext.Set<InvenItem>()
            .Where(x => x.LastModDateTime >= lastSyncDate)
            .ToListAsync();
        _logger.LogSuccess($"Success get {x1.Count} Items");
        
        return new IPostgresReadonlyStore.ContentForUpdate
        {
            Items = x1
        };
    }
    
    public async Task AddInvenItem(InvenItem item)
    {
        _logger.LogInfo($"Try add item {JsonConvert.SerializeObject(item)}"); 
        await _dbContext.Items.AddAsync(item, CancellationToken.None);
        _logger.LogInfo("Try save"); 
        await _dbContext.SaveChangesAsync();
        _logger.LogSuccess("Successfully saved"); 
    }
    
    public async Task<InvenItem?> GetInvenItem(Guid itemId)
    {
        _logger.LogInfo($"Try get item with id {itemId} from db"); 
        var result = await _dbContext.Set<InvenItem>().FirstOrDefaultAsync(x => x.InvenItemId == itemId, CancellationToken.None);
        _logger.LogSuccess("Success");
        return result;
    }
}