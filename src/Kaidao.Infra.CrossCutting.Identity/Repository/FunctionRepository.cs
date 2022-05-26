using Kaidao.Domain.IdentityEntity;
using Kaidao.Domain.Interfaces;
using Kaidao.Infra.CrossCutting.Identity.Context;
using Microsoft.EntityFrameworkCore;

namespace Kaidao.Infra.CrossCutting.Identity.Repository
{
    public class FunctionRepository : Repository<Function>, IFunctionRepository
    {
        public FunctionRepository(AuthDbContext context)
            : base(context)
        {
        }

        public override IQueryable<Function> GetAll()
        {
            return DbSet.AsNoTracking();
        }
    }
}