using Kaidao.Domain.Commands.Role;

namespace Kaidao.Domain.Validations.Role
{
    public class RemoveRoleCommandValidation : RoleValidation<RemoveRoleCommand>
    {
        public RemoveRoleCommandValidation()
        {
            ValidateId();
        }
    }
}