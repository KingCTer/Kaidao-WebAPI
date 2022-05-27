using Kaidao.Domain.IdentityEntity;

namespace Kaidao.Domain.Interfaces
{
    public interface IUserPermissionRepository : IRepository<UserPermission>
    {
        IQueryable<UserPermission> GetByAllow(string userId, bool isAllow);
        IQueryable<UserPermission> GetByUserId(string userId);
        UserPermission GetPermission(string userId, string functionId, string commandId);
        void Remove(UserPermission userPermission);
        void Update(string userId, string functionId, string commandId, bool allow);
        void Add(string userId, string functionId, string commandId, bool allow);
    }
}