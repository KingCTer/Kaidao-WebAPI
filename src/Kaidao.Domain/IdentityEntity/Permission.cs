using Kaidao.Domain.IdentityEntity;

namespace Kaidao.Domain.IdentityEntity
{
    public class Permission
    {
        public string RoleId { get; set; }
        public AppRole Role { get; set; }

        public string FunctionId { get; set; }
        public Function Function { get; set; }

        public string CommandId { get; set; }
        public Command Command { get; set; }
    }
}