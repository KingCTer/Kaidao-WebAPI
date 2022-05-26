using Kaidao.Domain.Commands.Role;

namespace Kaidao.Domain.Validations.Role
{
    public class RegisterNewRoleCommandValidation : RoleValidation<RegisterNewRoleCommand>
    {
        public RegisterNewRoleCommandValidation()
        {
            ValidateName();
        }
    }
}