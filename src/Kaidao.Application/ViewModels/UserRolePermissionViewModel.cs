namespace Kaidao.Application.ViewModels
{
    public class UserRolePermissionViewModel
    {
        public RoleViewModel CurrentRole { get; set; }
        
        public string UserId { get; set; }

        public List<CustomUserPermissions> UserPermission { get; set; }

        public List<PermissionUpdate> Permission { get; set; }

    }

    public class CustomUserPermissions
    {
        public FunctionViewModel Function { get; set; }
        public List<Tuple<CommandInFunctionViewModel, bool, string>> Permissions { get; set; }
    }

    public class PermissionUpdate
    {
        public string FunctionId { get; set; }
        public string CommandId { get; set; }
        public bool IsEnable { get; set; }
        public bool IsFactory { get; set; }

    }
}
