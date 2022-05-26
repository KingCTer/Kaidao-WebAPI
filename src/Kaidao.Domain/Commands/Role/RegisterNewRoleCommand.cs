using Kaidao.Domain.Validations.Role;

namespace Kaidao.Domain.Commands.Role
{
    public class RegisterNewRoleCommand : RoleCommand
    {
        public RegisterNewRoleCommand(string id, string name)
        {
            Id = id;
            Name = name;
            NormalizedName = name.ToUpper();
            IsSystemRole = false;
        }

        public override bool IsValid()
        {
            ValidationResult = new RegisterNewRoleCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}