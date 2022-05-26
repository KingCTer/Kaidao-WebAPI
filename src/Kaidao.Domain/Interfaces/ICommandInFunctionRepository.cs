using Kaidao.Domain.IdentityEntity;

namespace Kaidao.Domain.Interfaces
{
    public interface ICommandInFunctionRepository : IRepository<CommandInFunction>
    {
        IQueryable<CommandInFunction> GetByFunctionId(string functionId);
    }
}
