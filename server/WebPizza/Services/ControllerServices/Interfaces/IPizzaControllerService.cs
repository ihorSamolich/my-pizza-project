using WebPizza.ViewModels.Ingredient;
using WebPizza.ViewModels.Pizza;

namespace WebPizza.Services.ControllerServices.Interfaces
{
    public interface IPizzaControllerService
    {
        Task CreateAsync(PizzaCreateVm vm);
        Task UpdateAsync(PizzaEditVm vm);
        Task DeleteIfExistsAsync(int id);
    }
}
