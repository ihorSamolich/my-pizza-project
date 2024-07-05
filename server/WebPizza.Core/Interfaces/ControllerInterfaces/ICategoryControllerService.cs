using WebPizza.Core.DTO.Category;

namespace WebPizza.Core.Interfaces.ControllerInterfaces;
public interface ICategoryControllerService
{
    Task CreateAsync(CategoryCreateVm vm);
    Task UpdateAsync(CategoryEditVm vm);
    Task DeleteIfExistsAsync(int id);
}

