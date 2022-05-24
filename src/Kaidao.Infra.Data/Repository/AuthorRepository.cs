using Kaidao.Domain.AppEntity;
using Kaidao.Domain.Interfaces;
using Kaidao.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Kaidao.Infra.Data.Repository
{
    public class AuthorRepository : Repository<Author>, IAuthorRepository
    {
        public AuthorRepository(AppDbContext context)
            : base(context)
        {
        }

        public Author GetByName(string name)
        {
            return DbSet.AsNoTracking().FirstOrDefault(c => c.Name == name);
        }

        public override Author GetById(Guid id)
        {
            return DbSet.AsNoTracking().FirstOrDefault(c => c.Id == id);
        }
    }
}