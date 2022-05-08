using Kaidao.Domain.IdentityEntity;
using Kaidao.Domain.Interfaces;
using Kaidao.Infra.CrossCutting.Identity.Context;

namespace Kaidao.Infra.CrossCutting.Identity.Repository
{
    public class UserRepository : Repository<AppUser>, IUserRepository
    {
        public UserRepository(AuthDbContext context) : base(context)
        {
        }
    }
}