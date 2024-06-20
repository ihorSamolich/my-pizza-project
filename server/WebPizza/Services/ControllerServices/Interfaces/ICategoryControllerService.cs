using WebPizza.ViewModels.Category;

namespace WebPizza.Services.ControllerServices.Interfaces;

public interface ICategoryControllerService
{
    Task CreateAsync(CategoryCreateVm vm);
    Task UpdateAsync(CategoryEditVm vm);
    Task DeleteIfExistsAsync(int id);
}
