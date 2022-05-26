using Kaidao.Domain.IdentityEntity;
using Kaidao.Domain.Interfaces;
using Kaidao.Infra.CrossCutting.Identity.Context;
using Microsoft.EntityFrameworkCore;

namespace Kaidao.Infra.CrossCutting.Identity.Repository
{
    public class PermissionRepository : Repository<Permission>, IPermissionRepository
    {
        public PermissionRepository(AuthDbContext context)
            : base(context)
        {
        }

        public void Add(string roleId, string functionId, string commandId)
        {
            DbSet.Add(new Permission(roleId, functionId, commandId));
        }

        public IQueryable<Permission> GetByRoleId(IList<string> roles)
        {
            return DbSet.AsNoTracking().Where(p => roles.Contains(p.RoleId));
        }

        public IQueryable<Permission> GetByRoleId(string roleId)
        {
            return DbSet.AsNoTracking().Where(p => p.RoleId == roleId);
        }

        public Permission GetPermission(string roleId, string functionId, string commandId)
        {
            return DbSet.AsNoTracking().FirstOrDefault(p => p.RoleId == roleId && p.FunctionId == functionId && p.CommandId == commandId);
        }

        public void Remove(Permission permission)
        {
            DbSet.Remove(permission);
        }
    }
}