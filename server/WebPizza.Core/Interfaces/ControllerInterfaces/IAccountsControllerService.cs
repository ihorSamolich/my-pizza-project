using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebPizza.Core.Entities.Identity;

namespace WebPizza.Core.Interfaces.ControllerInterfaces;

public interface IAccountsControllerService
{
    Task<UserEntity> SignUpAsync(UserEntity user, string password);
}
