using Kaidao.Domain.AppEntity;

namespace Kaidao.Domain.Interfaces
{
    public interface IBookRepository : IRepository<Book>
    {
        Book GetByKey(string key);
    }
}