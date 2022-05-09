using Kaidao.Domain.Core.Commands;

namespace Kaidao.Domain.Commands.Author
{
    public abstract class AuthorCommand : Command
    {
        public Guid Id { get; protected set; }

        public string Name { get; protected set; }
    }
}