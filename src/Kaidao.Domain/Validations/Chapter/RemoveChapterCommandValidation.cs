using Kaidao.Domain.Commands.Book;
using Kaidao.Domain.Validations.Chapter;

namespace Kaidao.Domain.Validations.Book
{
	public class RemoveChapterCommandValidation : ChapterValidation<RemoveChapterCommand>
	{
		public RemoveChapterCommandValidation()
		{
			ValidateId();
		}
	}
}