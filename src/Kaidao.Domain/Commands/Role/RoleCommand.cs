using Kaidao.Domain.Core.Commands;

namespace Kaidao.Domain.Commands.Role;

public abstract class RoleCommand : Command
{
    public string Id { get; protected set; }

    public string Name { get; protected set; }

    public string NormalizedName { get; protected set; }

    public bool IsSystemRole { get; set; }
}