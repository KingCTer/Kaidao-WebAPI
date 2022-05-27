using Kaidao.Domain.IdentityEntity;

namespace Kaidao.Domain.Interfaces
{
    public interface IRoleRepository : IRepository<AppRole>
    {
        AppRole GetById(string id);
        AppRole GetByName(string name);

        void Remove(string id);
        void Add(string roleName);
    }
}