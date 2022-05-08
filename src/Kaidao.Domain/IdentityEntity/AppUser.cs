using Microsoft.AspNetCore.Identity;

namespace Kaidao.Domain.IdentityEntity
{
    public class AppUser : IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime Dob { get; set; }

        public IList<UserPermission> UserPermissions { get; set; }
    }
}
