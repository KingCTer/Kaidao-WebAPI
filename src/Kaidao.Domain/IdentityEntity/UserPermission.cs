namespace Kaidao.Domain.IdentityEntity
{
    public class UserPermission
    {
        public UserPermission()
        {
        }

        public UserPermission(string userId, string functionId, string commandId, bool allow)
        {
            UserId = userId;
            FunctionId = functionId;
            CommandId = commandId;
            Allow = allow;
        }

        public string UserId { get; set; }
        public AppUser User { get; set; }

        public string FunctionId { get; set; }
        public Function Function { get; set; }

        public string CommandId { get; set; }
        public Command Command { get; set; }

        public bool Allow { get; set; }
    }
}