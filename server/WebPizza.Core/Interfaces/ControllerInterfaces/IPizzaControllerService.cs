
using WebPizza.Core.DTO.Pizza;

namespace WebPizza.Core.Interfaces.ControllerInterfaces;

public interface IPizzaControllerService
{
    Task CreateAsync(PizzaCreateVm vm);
    Task UpdateAsync(PizzaEditVm vm);
    Task DeleteIfExistsAsync(int id);
}