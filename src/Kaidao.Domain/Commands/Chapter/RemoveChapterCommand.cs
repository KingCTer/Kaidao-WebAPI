using Kaidao.Domain.Commands.Chapter;
using Kaidao.Domain.Validations.Chapter;

namespace Kaidao.Domain.Commands.Book
{
    public class RemoveChapterCommand : ChapterCommand
    {

        public RemoveChapterCommand(Guid id)
        {
            Id = id;
            AggregateId = id;
        }

        public override bool IsValid()
        {
            ValidationResult = new RemoveChapterCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }

    }
}
