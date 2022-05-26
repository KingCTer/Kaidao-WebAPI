using Kaidao.Application.ViewModels;

namespace Kaidao.Web.Admin.ViewModels
{
    public class RoleGroupViewModel
    {
        public List<RoleWithPermissionViewModel> Index { get; set; }

        public string RoleUpdateId { get; set; }
        public string RoleCurrentName { get; set; }
        public string RoleUpdateName { get; set; }
        public List<PermissionUpdate> Permission { get; set; }
    }

    public class PermissionUpdate
    {
        public string FunctionId { get; set; }
        public string CommandId { get; set; }
        public bool IsEnable { get; set; }
    }
}
