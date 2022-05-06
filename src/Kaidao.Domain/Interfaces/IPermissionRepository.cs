using Kaidao.Domain.IdentityEntity;

namespace Kaidao.Domain.Interfaces
{
    public interface IPermissionRepository : IRepository<Permission>
    {
        IQueryable<Permission> GetByRoleId(IList<string> roles);
    }
}