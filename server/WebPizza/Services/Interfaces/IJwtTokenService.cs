using WebPizza.Data.Entities.Identity;

namespace WebPizza.Services.Interfaces;

public interface IJwtTokenService
{
    Task<string> CreateTokenAsync(UserEntity user);
}
