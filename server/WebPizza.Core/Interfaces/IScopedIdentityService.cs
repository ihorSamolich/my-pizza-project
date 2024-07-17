using Microsoft.AspNetCore.Mvc;
using WebPizza.Core.Entities.Identity;

namespace WebPizza.Core.Interfaces;

public interface IScopedIdentityService
{
    UserEntity? User { get; }

    Task InitCurrentUserAsync(ControllerBase controller);

    UserEntity GetRequiredUser();

    long GetRequiredUserId();
}