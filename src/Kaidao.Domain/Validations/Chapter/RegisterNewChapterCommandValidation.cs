using Kaidao.Domain.Commands.Chapter;

namespace Kaidao.Domain.Validations.Chapter
{
    public class RegisterNewChapterCommandValidation : ChapterValidation<RegisterNewChapterCommand>
    {
        public RegisterNewChapterCommandValidation()
        {
            ValidateName();
        }
    }
}