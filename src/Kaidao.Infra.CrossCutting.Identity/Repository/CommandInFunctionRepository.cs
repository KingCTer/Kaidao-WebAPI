using Kaidao.Domain.IdentityEntity;
using Kaidao.Domain.Interfaces;
using Kaidao.Infra.CrossCutting.Identity.Context;
using Microsoft.EntityFrameworkCore;

namespace Kaidao.Infra.CrossCutting.Identity.Repository
{
    public class CommandInFunctionRepository : Repository<CommandInFunction>, ICommandInFunctionRepository
    {
        public CommandInFunctionRepository(AuthDbContext context)
            : base(context)
        {
        }

        public IQueryable<CommandInFunction> GetByFunctionId(string functionId)
        {
            return DbSet.AsNoTracking().Where(p => p.FunctionId == functionId);
        }
    }
}