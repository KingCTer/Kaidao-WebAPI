using Kaidao.Domain.Core.Entity;

namespace Kaidao.Domain.AppEntity
{
    public class Chapter : Entity
    {
        public Chapter()
        {
        }

        public Chapter(Guid id, int order, string name, string url, string content, Guid bookId)
        {
            Id = id;
            Order = order;
            Name = name;
            Url = url;
            Content = content;
            BookId = bookId;
        }

        public Chapter(Guid id, Guid bookId, int order, string name, string url)
        {
            Id = id;
            Order = order;
            Name = name;
            Url = url;
            BookId = bookId;
        }

        public int Order { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string Content { get; set; }

        public Guid BookId { get; set; }
        public Book Book { get; set; }
    }
}