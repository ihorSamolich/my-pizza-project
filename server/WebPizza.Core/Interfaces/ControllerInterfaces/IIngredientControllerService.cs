using WebPizza.Core.DTO.Ingredient;

namespace WebPizza.Core.Interfaces.ControllerInterfaces;

public interface IIngredientControllerService
{
    Task CreateAsync(IngredientCreateVm vm);
    Task UpdateAsync(IngredientEditVm vm);
    Task DeleteIfExistsAsync(int id);
}
