using Microsoft.AspNetCore.Identity;

namespace WebPizza.Core.Entities.Identity;

public class RoleEntity : IdentityRole<int>
{
    public virtual ICollection<UserRoleEntity> UserRoles { get; set; } = null!;
}
