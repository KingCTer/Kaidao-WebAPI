using Kaidao.Domain.Validations.Book;

namespace Kaidao.Domain.Commands.Chapter
{
	public class UpdateChapterCommand : ChapterCommand
	{
		public UpdateChapterCommand(Guid id, int order, string name, string url, string content, Guid bookId)
		{
			Id = id;
			Order = order;
			Name = name;
			Url = url;
			Content = content;
			BookId = bookId;
		}

		public override bool IsValid()
		{
			ValidationResult = new UpdateChapterCommandValidation().Validate(this);
			return ValidationResult.IsValid;
		}
	}
}