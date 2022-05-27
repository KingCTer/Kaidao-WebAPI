using Kaidao.Application.ViewModels;

namespace Kaidao.Web.Admin.ViewModels
{
    public class UserRoleWithPermissionViewModel
    {
        public RoleViewModel CurrentRole { get; set; }

        public List<CustomUserPermissions> UserPermission { get; set; }

    }

    public class CustomUserPermissions
    {
        public FunctionViewModel Function { get; set; }
        public List<Tuple<CommandInFunctionViewModel, bool, string>> Permissions { get; set; }
    }
}
