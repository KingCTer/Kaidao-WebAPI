using Kaidao.Domain.IdentityEntity;
using Kaidao.Domain.Interfaces;
using Kaidao.Infra.CrossCutting.Identity.Context;
using Microsoft.EntityFrameworkCore;

namespace Kaidao.Infra.CrossCutting.Identity.Repository
{
    public class UserPermissionRepository : Repository<UserPermission>, IUserPermissionRepository
    {
        public UserPermissionRepository(AuthDbContext context)
            : base(context)
        {
        }

        public IQueryable<UserPermission> GetByAllow(string userId, bool isAllow)
        {
            return DbSet.AsNoTracking().Where(up => up.UserId == userId && up.Allow == isAllow);
        }

        public IQueryable<UserPermission> GetByUserId(string userId)
        {
            return DbSet.AsNoTracking().Where(up => up.UserId == userId);
        }

        public UserPermission GetPermission(string userId, string functionId, string commandId)
        {
            return DbSet.AsNoTracking().FirstOrDefault(up => up.UserId == userId && up.FunctionId == functionId && up.CommandId == commandId);
        }

        public void Remove(UserPermission userPermission)
        {
            DbSet.Remove(userPermission);
        }

        public void Update(string userId, string functionId, string commandId, bool allow)
        {
            DbSet.Update(new UserPermission(userId, functionId, commandId, allow));
        }

        public void Add(string userId, string functionId, string commandId, bool allow)
        {
            DbSet.Add(new UserPermission(userId, functionId, commandId, allow));
        }
    }
}