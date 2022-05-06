using Kaidao.Domain.IdentityEntity;

namespace Kaidao.Domain.Interfaces
{
    public interface IUserPermissionRepository : IRepository<UserPermission>
    {
        IQueryable<UserPermission> GetByAllow(string userId, bool isAllow);
    }
}