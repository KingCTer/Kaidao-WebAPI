using FluentValidation;
using Kaidao.Domain.Commands.Book;

namespace Kaidao.Domain.Validations.Book;

public abstract class BookValidation<T> : AbstractValidator<T> where T : BookCommand
{
    protected void ValidateName()
    {
        RuleFor(c => c.Name)
            .NotEmpty().WithMessage("Please ensure you have entered the Name");
    }

    protected void ValidateId()
    {
        RuleFor(c => c.Id)
            .NotEqual(Guid.Empty);
    }
}