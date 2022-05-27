using Kaidao.Domain.IdentityEntity;
using Kaidao.Domain.Interfaces;
using Kaidao.Infra.CrossCutting.Identity.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Kaidao.Infra.CrossCutting.Identity.Repository
{
    public class UserRoleRepository : Repository<IdentityUserRole<string>>, IUserRoleRepository
    {
        public UserRoleRepository(AuthDbContext context)
            : base(context)
        {
        }

        public IdentityUserRole<string> GetByUserId(string userId)
        {
            return DbSet.AsNoTracking().FirstOrDefault(u => u.UserId == userId);
        }
    }
}