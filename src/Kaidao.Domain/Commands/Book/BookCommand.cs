using Kaidao.Domain.Core.Commands;

namespace Kaidao.Domain.Commands.Book
{
    public abstract class BookCommand : Command
    {
        public Guid Id { get; protected set; }

        public string Name { get; protected set; }
        public string Key { get; protected set; }
        public string Cover { get; protected set; }
        public string Status { get; protected set; }
        public int View { get; protected set; }
        public int Like { get; protected set; }
        public int ChapterTotal { get; set; }
        public string Intro { get; set; }

        public Guid AuthorId { get; protected set; }

        public Guid CategoryId { get; protected set; }
    }
}