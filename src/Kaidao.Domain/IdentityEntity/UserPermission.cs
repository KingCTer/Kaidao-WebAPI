namespace Kaidao.Domain.IdentityEntity
{
    public class UserPermission
    {
        public string UserId { get; set; }
        public AppUser User { get; set; }

        public string FunctionId { get; set; }
        public Function Function { get; set; }

        public string CommandId { get; set; }
        public Command Command { get; set; }

        public bool Allow { get; set; }
    }
}