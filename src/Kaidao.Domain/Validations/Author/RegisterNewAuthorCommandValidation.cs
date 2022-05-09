using Kaidao.Domain.Commands.Author;

namespace Kaidao.Domain.Validations.Author
{
    public class RegisterNewAuthorCommandValidation : AuthorValidation<RegisterNewAuthorCommand>
    {
        public RegisterNewAuthorCommandValidation()
        {
            ValidateName();
        }
    }
}