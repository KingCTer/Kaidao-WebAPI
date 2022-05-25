using Kaidao.Domain.Commands.Book;

namespace Kaidao.Domain.Validations.Book
{
    public class RemoveBookCommandValidation : BookValidation<RemoveBookCommand>
    {
        public RemoveBookCommandValidation()
        {
            ValidateId();
        }
    }
}