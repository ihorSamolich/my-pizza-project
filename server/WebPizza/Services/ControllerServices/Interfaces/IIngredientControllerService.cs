using WebPizza.ViewModels.Category;
using WebPizza.ViewModels.Ingredient;

namespace WebPizza.Services.ControllerServices.Interfaces
{
    public interface IIngredientControllerService
    {
        Task CreateAsync(IngredientCreateVm vm);
        Task UpdateAsync(IngredientEditVm vm);
        Task DeleteIfExistsAsync(int id);
    }
}
