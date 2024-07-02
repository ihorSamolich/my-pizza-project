using WebPizza.Data.Entities.Identity;
using WebPizza.ViewModels.Account;

namespace WebPizza.Services.ControllerServices.Interfaces
{
    public interface IAccountsControllerService
    {
        Task<UserEntity> SignUpAsync(RegisterVm vm);
    }
}
