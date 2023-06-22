using Dictionary.Application.Models;

namespace Dictionary.Application.Interfaces.UseCases;

public interface IUpdateInvenItemUseCase
{
    Task Execute(InvenItem item);
}