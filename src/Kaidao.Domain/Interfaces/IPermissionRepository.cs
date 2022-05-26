using Kaidao.Domain.IdentityEntity;

namespace Kaidao.Domain.Interfaces
{
    public interface IPermissionRepository : IRepository<Permission>
    {
        IQueryable<Permission> GetByRoleId(IList<string> roles);

        IQueryable<Permission> GetByRoleId(string roleId);

        Permission GetPermission(string roleId, string functionId, string commandId);

        void Remove(Permission permission);

        void Add(string roleId, string functionId, string commandId);


    }
}