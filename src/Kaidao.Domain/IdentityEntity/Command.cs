namespace Kaidao.Domain.IdentityEntity
{
    public class Command
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public IList<CommandInFunction> CommandInFunctions { get; set; }
        public IList<Permission> Permissions { get; set; }
        public IList<UserPermission> UserPermissions { get; set; }
    }
}