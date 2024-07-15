using WebPizza.Core.DTO;
using WebPizza.Core.Entities.Identity;

namespace WebPizza.Core.Interfaces.ControllerInterfaces;

public interface IOrderControllerService
{
    Task CreateAsync(OrderCreateVm vm, UserEntity user);

    //Task UpdateAsync(OrderEditVm vm);
    Task DeleteIfExistsAsync(int id);
}
