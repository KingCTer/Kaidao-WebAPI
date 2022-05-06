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

        public IQueryable<Permission> GetByRoleId(IList<string> roles)
        {
            return DbSet.AsNoTracking().Where(p => roles.Contains(p.RoleId));
        }
    }
}