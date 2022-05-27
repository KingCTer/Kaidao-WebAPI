using Kaidao.Domain.IdentityEntity;
using Kaidao.Domain.Interfaces;
using Kaidao.Infra.CrossCutting.Identity.Context;
using Microsoft.EntityFrameworkCore;

namespace Kaidao.Infra.CrossCutting.Identity.Repository
{
    public class RoleRepository : Repository<AppRole>, IRoleRepository
    {
        public RoleRepository(AuthDbContext context)
            : base(context)
        {
        }


        public AppRole GetById(string id)
        {
            return DbSet.AsNoTracking().FirstOrDefault(r => r.Id == id);
        }

        public AppRole GetByName(string name)
        {
            return DbSet.AsNoTracking().FirstOrDefault(r => r.Name == name);
        }

        public void Remove(string id)
        {
            DbSet.Remove(GetById(id));
        }

        public void Add(string roleName)
        {
            var appRole = new AppRole
            {
                Id = roleName,
                Name = roleName,
                NormalizedName = roleName.ToUpper(),
                IsSystemRole = false,
            };

            DbSet.Add(appRole);
        }
    }
}