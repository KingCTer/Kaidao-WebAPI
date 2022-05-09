using FluentValidation;
using Kaidao.Domain.Commands.Chapter;

namespace Kaidao.Domain.Validations.Chapter
{
    public abstract class ChapterValidation<T> : AbstractValidator<T> where T : ChapterCommand
    {
        protected void ValidateName()
        {
        }

        protected void ValidateId()
        {
            RuleFor(c => c.Id)
                .NotEqual(Guid.Empty);
        }
    }
}