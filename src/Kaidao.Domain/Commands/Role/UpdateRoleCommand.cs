
using Kaidao.Domain.Validations.Role;

namespace Kaidao.Domain.Commands.Role
{
    public class UpdateRoleCommand : RoleCommand
    {
        public UpdateRoleCommand(string id, string name)
        {
            Id = id;
            Name = name;
            NormalizedName = name.ToUpper();
            IsSystemRole = false;
        }

    public override bool IsValid()
        {
            ValidationResult = new UpdateRoleCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}