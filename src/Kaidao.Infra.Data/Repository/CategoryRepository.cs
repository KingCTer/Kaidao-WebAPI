using Kaidao.Domain.AppEntity;
using Kaidao.Domain.Interfaces;
using Kaidao.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Kaidao.Infra.Data.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext context)
            : base(context)
        {
        }

        public Category GetByName(string name)
        {
            return DbSet.AsNoTracking().FirstOrDefault(c => c.Name == name);
        }
    }
}