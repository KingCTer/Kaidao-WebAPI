using Kaidao.Domain.IdentityEntity;

namespace Kaidao.Domain.Interfaces
{
    public interface IUserPermissionRepository : IRepository<UserPermission>
    {
        IQueryable<UserPermission> GetByAllow(bool isAllow);
    }
}