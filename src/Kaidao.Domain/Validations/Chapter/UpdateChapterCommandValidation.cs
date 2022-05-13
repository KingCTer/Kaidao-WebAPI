using Kaidao.Domain.Commands.Chapter;
using Kaidao.Domain.Validations.Chapter;

namespace Kaidao.Domain.Validations.Book
{
	public class UpdateChapterCommandValidation : ChapterValidation<UpdateChapterCommand>
    {
        public UpdateChapterCommandValidation()
        {
            ValidateId();
            ValidateName();
        }
    }
}