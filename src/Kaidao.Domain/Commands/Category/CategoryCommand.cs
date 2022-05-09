using Kaidao.Domain.Core.Commands;

namespace Kaidao.Domain.Commands.Category
{
    public abstract class CategoryCommand : Command
    {
        public Guid Id { get; protected set; }

        public string Name { get; protected set; }
    }
}