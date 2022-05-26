using Kaidao.Domain.Commands.Book;
using Kaidao.Domain.Commands.Role;

namespace Kaidao.Domain.Validations.Chapter
{
    public class RemoveChapterCommandValidation : ChapterValidation<RemoveChapterCommand>
    {
        public RemoveChapterCommandValidation()
        {
            ValidateId();
        }
    }
}