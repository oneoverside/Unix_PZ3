using Dictionary.Application.Models;

namespace Dictionary.Application.Interfaces.UseCases;

public interface IGetItemFromRedisUseCase
{
    public Task<InvenItem?> Execute(Guid itemId);
}