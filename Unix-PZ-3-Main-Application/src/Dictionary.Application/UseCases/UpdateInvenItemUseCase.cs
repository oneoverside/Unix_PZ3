using Dictionary.Application.Interfaces.Stores;
using Dictionary.Application.Interfaces.UseCases;
using Dictionary.Application.Models;

namespace Dictionary.Application.UseCases;

class UpdateInvenItemUseCase : IUpdateInvenItemUseCase
{
    private readonly IPostgresReadonlyStore _store;
    private readonly FileSystemLogger _logger;

    public UpdateInvenItemUseCase(IPostgresReadonlyStore store, FileSystemLogger logger)
    {
        _store = store;
        _logger = logger;
    }

    public async Task Execute(InvenItem item)
    {
        try
        {
            _logger.LogInfo("Try add new item into DB");
            await _store.AddInvenItem(item);
            _logger.LogSuccess("Success!");
        }
        catch(Exception ex)
        {
            _logger.LogError("Something go wrong", ex);
        }
    }
}