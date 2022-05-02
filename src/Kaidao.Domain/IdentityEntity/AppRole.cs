using Microsoft.AspNetCore.Identity;

namespace Kaidao.Domain.IdentityEntity
{
    public class AppRole : IdentityRole
    {
        public bool IsSystemRole { get; set; }

        public IList<Permission> Permissions { get; set; }
    }
}
