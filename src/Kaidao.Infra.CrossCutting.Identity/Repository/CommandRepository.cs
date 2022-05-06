using Kaidao.Domain.IdentityEntity;
using Kaidao.Domain.Interfaces;
using Kaidao.Infra.CrossCutting.Identity.Context;

namespace Kaidao.Infra.CrossCutting.Identity.Repository
{
    public class CommandRepository : Repository<Command>, ICommandRepository
    {
        public CommandRepository(AuthDbContext context)
            : base(context)
        {
        }
    }
}