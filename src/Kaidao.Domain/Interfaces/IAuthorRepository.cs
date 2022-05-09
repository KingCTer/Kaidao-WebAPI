using Kaidao.Domain.AppEntity;

namespace Kaidao.Domain.Interfaces
{
    public interface IAuthorRepository : IRepository<Author>
    {
        Author GetByName(string name);
    }
}
