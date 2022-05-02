namespace Kaidao.Domain.IdentityEntity
{
    public class Function
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string ParentId { get; set; }
        public Function Parent { get; set; }
        public ICollection<Function> Functions { get; set; }

        public IList<CommandInFunction> CommandInFunctions { get; set; }
        public IList<Permission> Permissions { get; set; }
        public IList<UserPermission> UserPermissions { get; set; }

    }
}