using Kaidao.Application.Responses;
using Kaidao.Application.ViewModels;

namespace Kaidao.Application.AppServices.Interfaces
{
    public interface IRoleAppService : IDisposable
    {
        public RoleViewModel GetById(string roleId);

        public RoleViewModel GetByName(string roleName);

        public IEnumerable<RoleViewModel> GetAll();

        List<RoleWithPermissionViewModel> GetAllWithPermission();

        UserRolePermissionViewModel GetRoleWithPermission(string roleId);
    }
}