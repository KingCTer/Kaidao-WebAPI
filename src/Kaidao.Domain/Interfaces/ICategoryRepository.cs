using Kaidao.Domain.AppEntity;

namespace Kaidao.Domain.Interfaces
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Category GetByName(string name);
    }
}