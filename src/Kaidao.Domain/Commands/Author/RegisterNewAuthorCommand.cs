using Kaidao.Domain.Validations.Author;

namespace Kaidao.Domain.Commands.Author
{
    public class RegisterNewAuthorCommand : AuthorCommand
    {
        public RegisterNewAuthorCommand(string name)
        {
            Name = name;
        }

        public override bool IsValid()
        {
            ValidationResult = new RegisterNewAuthorCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}