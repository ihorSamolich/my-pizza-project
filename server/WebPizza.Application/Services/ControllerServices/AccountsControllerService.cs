using Microsoft.AspNetCore.Identity;
using WebPizza.Core.Constants;
using WebPizza.Core.Entities.Identity;
using WebPizza.Core.Interfaces.ControllerInterfaces;
using WebPizza.Core.Interfaces;
using WebPizza.Infrastructure.Data;

namespace WebPizza.Application.Services.ControllerServices;

public class AccountsControllerService(
    PizzaDbContext context,
    UserManager<UserEntity> userManager
    ) : IAccountsControllerService
{
    public async Task<UserEntity> SignUpAsync(UserEntity user, string password)
    {
        try
        {
            await CreateUserAsync(user, password);
        }
        catch
        {
            throw;
        }

        return user;
    }

    private async Task CreateUserAsync(UserEntity user, string? password = null)
    {
        using var transaction = await context.Database.BeginTransactionAsync();

        try
        {
            IdentityResult identityResult = await CreateUserInDatabaseAsync(user, password);
            if (!identityResult.Succeeded)
                throw new Exception("User creating error");

            identityResult = await userManager.AddToRoleAsync(user, Roles.User);
            if (!identityResult.Succeeded)
                throw new Exception("Role assignment error");

            await transaction.CommitAsync();
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    private async Task<IdentityResult> CreateUserInDatabaseAsync(UserEntity user, string? password)
    {
        if (password is null)
            return await userManager.CreateAsync(user);

        return await userManager.CreateAsync(user, password);
    }
}
