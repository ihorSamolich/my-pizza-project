using WebPizza.Core.Entities.Identity;

namespace WebPizza.Core.Interfaces;

public interface IJwtTokenService
{
    Task<string> CreateTokenAsync(UserEntity user);
}
