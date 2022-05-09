using Kaidao.Domain.Core.Entity;

namespace Kaidao.Domain.AppEntity
{
    public class Author : Entity
    {
        public Author()
        {
        }

        public Author(Guid id, string name)
        {
            Id = id;
            Name = name;
        }

        public string Name { get; set; }

        public ICollection<Book> Books { get; set; }
    }
}