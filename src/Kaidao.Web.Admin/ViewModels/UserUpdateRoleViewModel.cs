using Kaidao.Application.ViewModels;

namespace Kaidao.Web.Admin.ViewModels
{
    public class UserUpdateRoleViewModel
    {
        public string UserId { get; set; }

        public string RoleId { get; set; }

        public string UpdateRoleId { get; set; }

        public List<RoleViewModel> RoleList { get; set; }
    }
}
