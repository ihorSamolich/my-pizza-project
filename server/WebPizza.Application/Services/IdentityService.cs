using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebPizza.Core.Entities.Identity;
using WebPizza.Core.Interfaces;

namespace WebPizza.Application.Services;

public class IdentityService(
  UserManager<UserEntity> userManager
  ) : IIdentityService
{

    public async Task<UserEntity> GetCurrentUserAsync(ControllerBase controller)
    {
        string email = controller.User.Claims
                .FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress")
                ?.Value
                ?? throw new Exception("User error");

        UserEntity user = await userManager
            .FindByEmailAsync(email)
            ?? throw new Exception("User error");

        return user;
    }
}
