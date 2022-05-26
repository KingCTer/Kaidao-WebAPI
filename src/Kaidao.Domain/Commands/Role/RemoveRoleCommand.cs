using Kaidao.Domain.Validations.Role;

namespace Kaidao.Domain.Commands.Role
{
    public class RemoveRoleCommand : RoleCommand
    {

        public RemoveRoleCommand(string id)
        {
            Id = id;
        }

        public override bool IsValid()
        {
            ValidationResult = new RemoveRoleCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }

    }
}
