using Microsoft.AspNetCore.Mvc;
using WebPizza.Core.Entities.Identity;
using WebPizza.Core.Interfaces;

namespace WebPizza.Application.Services;

public class ScopedIdentityService(
    IIdentityService identityService
) : IScopedIdentityService
{

    public UserEntity? User { get; private set; } = null;

    public async Task InitCurrentUserAsync(ControllerBase controller)
    {
        User = await identityService.GetCurrentUserAsync(controller);
    }

    public UserEntity GetRequiredUser() =>
        User ?? throw new Exception($"User in {nameof(ScopedIdentityService)} is not inicialized");

    public long GetRequiredUserId() => GetRequiredUser().Id;
}