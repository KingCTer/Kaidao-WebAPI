using Kaidao.Domain.AppEntity;
using Kaidao.Domain.Interfaces;
using Kaidao.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Kaidao.Infra.Data.Repository
{
    public class BookRepository : Repository<Book>, IBookRepository
    {
        public BookRepository(AppDbContext context)
            : base(context)
        {
        }

        public Book GetByKey(string key)
        {
            return DbSet.AsNoTracking().FirstOrDefault(c => c.Key == key);
        }

        public override Book GetById(Guid id)
        {
            return DbSet.AsNoTracking().FirstOrDefault(c => c.Id == id);
        }
    }
}