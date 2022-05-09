using Kaidao.Domain.Validations.Book;

namespace Kaidao.Domain.Commands.Book
{
    public class UpdateBookCommand : BookCommand
    {
        public UpdateBookCommand(Guid id,
                                 string name,
                                 string key,
                                 string cover,
                                 string status,
                                 int view,
                                 int like,
                                 Guid authorId,
                                 Guid categoryId,
                                 int chapterTotal,
                                      string intro
            )
        {
            Id = id;
            Name = name;
            Key = key;
            Cover = cover;
            Status = status;
            View = view;
            Like = like;
            AuthorId = authorId;
            CategoryId = categoryId;
            ChapterTotal = chapterTotal;
            Intro = intro;
        }

        public override bool IsValid()
        {
            ValidationResult = new UpdateBookCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}