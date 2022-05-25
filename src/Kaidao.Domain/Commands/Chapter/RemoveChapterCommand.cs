using Kaidao.Domain.Commands.Chapter;
using Kaidao.Domain.Validations.Book;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
