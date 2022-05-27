using Kaidao.Domain.IdentityEntity;
using Microsoft.AspNetCore.Identity;

namespace Kaidao.Domain.Interfaces
{
    public interface IUserRoleRepository : IRepository<IdentityUserRole<string>>
    {
        IdentityUserRole<string> GetByUserId(string userId);
        void Update(string userId, string roleId, string updateRoleId);
    }
}