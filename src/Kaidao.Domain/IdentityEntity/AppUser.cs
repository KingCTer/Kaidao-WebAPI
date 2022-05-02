using Microsoft.AspNetCore.Identity;

namespace Kaidao.Domain.IdentityEntity
{
    public class AppUser : IdentityUser
    {
        public IList<UserPermission> UserPermissions { get; set; }
    }
}
