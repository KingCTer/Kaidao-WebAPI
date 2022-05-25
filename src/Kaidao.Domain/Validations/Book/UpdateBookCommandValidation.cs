using Kaidao.Domain.Commands.Book;

namespace Kaidao.Domain.Validations.Book
{
    public class UpdateBookCommandValidation : BookValidation<UpdateBookCommand>
    {
        public UpdateBookCommandValidation()
        {
            ValidateId();
            ValidateName();
        }
    }
}