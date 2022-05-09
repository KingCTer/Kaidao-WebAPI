using Kaidao.Domain.Commands.Book;

namespace Kaidao.Domain.Validations.Book
{
    public class RegisterNewBookCommandValidation : BookValidation<RegisterNewBookCommand>
    {
        public RegisterNewBookCommandValidation()
        {
            ValidateName();
        }
    }
}