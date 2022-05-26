using Kaidao.Domain.Commands.Role;

namespace Kaidao.Domain.Validations.Role
{
    public class UpdateRoleCommandValidation : RoleValidation<UpdateRoleCommand>
    {
        public UpdateRoleCommandValidation()
        {
            ValidateId();
            ValidateName();
        }
    }
}